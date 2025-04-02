using System.Drawing;
using System.Drawing.Imaging;
using Calendar.Api.DatabaseConnection;
using Calendar.Api.DTO;
using Calendar.Api.Models;
using Calendar.Api.Services;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using BC = BCrypt.Net.BCrypt;
using ImageSharpImage = SixLabors.ImageSharp.Image;
namespace Calendar.Api.Logics
{
    public class UserAssignmentLogic
    {
        private readonly CalendarContext _context;
        private readonly FilePath _filePath;
        private readonly Base64Resizer _base64Resizer;
        public UserAssignmentLogic(CalendarContext context, FilePath filePath, Base64Resizer base64Resizer)
        {
            _context = context;
            _filePath = filePath;
            _base64Resizer = base64Resizer;
        }

        public async Task<UserAssignmentDTO_GET> LoginUser(UserAssignmentDTO_LOGIN dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Email == dto.Email);
            if (user != null && BC.Verify(dto.Password, user.Password))
            {
                var data = await (from a in _context.UserAssignments
                                  join u in _context.Users on a.UserId equals u.UserId
                                  join r in _context.Roles on a.RoleId equals r.RoleId
                                  where a.IsDisabled == false
                                  select new UserAssignmentDTO_GET
                                  {
                                      UserAssignmentId = a.UserAssignmentId,
                                      UserId = a.UserId,
                                      RoleId = a.RoleId,
                                      RoleName = r.Name,
                                      RoleLevel = r.RoleLevel,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      Email = u.Email,
                                      Image = _base64Resizer.ResizeImage(_filePath.UserImagePath(u.Image)),
                                  }).FirstOrDefaultAsync();
                return data;
            }
            return null;
        }

        public async Task<string> RegisterUser(UserAssignmentDTO_POST dto)
        {
            int success = 0;
            Guid userId = Guid.NewGuid();
            Guid userAssignmentId = Guid.NewGuid();
            string imageName = "";

            var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if(checkUser != null)
            {
                return "exists";
            }

            if (dto.Image != null)
            {
                imageName = userId.ToString() + Path.GetExtension(dto.Image.FileName);
                var profilePath = _filePath.UserImagePath("");
                string profileFilePath = Path.Combine(profilePath, imageName);

                using (var image = ImageSharpImage.Load(dto.Image.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(200, 200));
                    image.Save(profileFilePath);
                }
            }

            var user = new User();
            user.UserId = userId;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Password = BC.HashPassword(dto.Password, BC.GenerateSalt());
            user.Image = imageName;

            _context.Users.Add(user);
            success = await _context.SaveChangesAsync();

            if(success > 0)
            {
                var userAssignment = new UserAssignment();
                userAssignment.UserAssignmentId = userAssignmentId;
                userAssignment.UserId = userId;
                userAssignment.RoleId = new Guid("575f0e52-7ae1-4b3c-8d60-172b841682f6"); // Regular User ID
                userAssignment.DateTimeCreated = DateTime.Now;

                _context.UserAssignments.Add(userAssignment);
                await _context.SaveChangesAsync();

                return "success";
            }
            return "error";
        }

        public async Task<bool> UpdateUser(Guid userId, UserAssignmentDTO_POST dto)
        {

            int success = 0;
            string profileName = "";

            var data = _context.Users.FirstOrDefault(x => x.UserId == userId);

            // UPDATE IMAGE LOGIC
            if (dto.Image != null && dto.Image.Length > 0)
            {
                if (File.Exists(_filePath.UserImagePath(data.Image)))
                {
                    File.Delete(_filePath.UserImagePath(data.Image));
                }

                profileName = userId.ToString() + Path.GetExtension(dto.Image.FileName);
                data.Image = profileName;
                var profilePath = _filePath.UserImagePath("");
                string profileFilePath = Path.Combine(profilePath, profileName);

                using (var image = System.Drawing.Image.FromStream(dto.Image.OpenReadStream()))
                using (var resizedImage = new Bitmap(200, 200))
                using (var graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(image, 0, 0, 200, 200);
                    resizedImage.Save(profileFilePath, ImageFormat.Png);
                }
            }

            // PASSWORD
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                data.Password = BC.HashPassword(dto.Password, BC.GenerateSalt());
            }

            data.FirstName = dto.FirstName;
            data.LastName = dto.LastName;

            _context.Users.Update(data);
            success = _context.SaveChanges();

            if (success > 0)
            {
                return true;
            }
            return false;
        }
    }
}

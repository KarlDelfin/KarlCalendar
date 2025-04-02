namespace Calendar.Api.Services
{
    public class FilePath
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FilePath(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string UserImagePath(string fileName)
        {
            return $"{_webHostEnvironment.WebRootPath}/Images/User/{fileName}";
        }
    }
}

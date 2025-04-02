using Calendar.Api.DatabaseConnection;
using Calendar.Api.Models;
using Calendar.API.DTO;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api.Logics
{
    public class SharedCalendarLogic
    {
        private readonly CalendarContext _context;
        public SharedCalendarLogic(CalendarContext context)
        {
            _context = context;
        }

        // GET ALL USERS (DONE!)
        public async Task<List<SharedCalendarDTO_GET>> GetAllSharedCalendarByCalendarId(Guid calendarId, string? search = "")
        {
            var data = await (from u in _context.Users
                        join sc in _context.SharedCalendars on u.UserId equals sc.SharedUserId
                        where sc.CalendarId == calendarId

                        select new SharedCalendarDTO_GET
                        {
                            SharedCalendarId = sc.CalendarId,
                            CalendarId = calendarId,
                            SharedUserId = sc.SharedUserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email
                        }).ToListAsync();

            if (!string.IsNullOrWhiteSpace(search) )
            {
                data = (from ur in _context.UserAssignments
                        join u in _context.Users on ur.UserId equals u.UserId
                        from sc in _context.SharedCalendars.Where(x => x.SharedUserId == ur.UserId && x.CalendarId == calendarId).DefaultIfEmpty()
                        where ur.UserId != sc.SharedUserId
                        select new SharedCalendarDTO_GET
                        {
                            SharedCalendarId = sc.SharedCalendarId,
                            CalendarId = calendarId,
                            SharedUserId = ur.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email
                        }).Where(x => x.Email.Contains(search)).ToList();
                return data;
            }
            return data;
        }

        // GRANT USER
        public async Task<bool> GrantUser(SharedCalendarDTO_POST dto)
        {
            int success = 0;
            var data = new SharedCalendar();

            data.SharedCalendarId = Guid.NewGuid();
            data.SharedUserId = dto.SharedUserId;
            data.CalendarId = dto.CalendarId;
            data.DateTimeCreated = DateTime.Now;

            _context.SharedCalendars.Add(data);
            success = await _context.SaveChangesAsync();

            if (success > 0)
            {
                return true;
            }
            return false;
        }

        // REVOKE USER
        public async Task<bool> RevokeUser(Guid sharedCalendarId)
        {
            int success = 0;
            var data = await _context.SharedCalendars.FirstOrDefaultAsync(x => x.SharedCalendarId == sharedCalendarId);
            _context.SharedCalendars.Remove(data);
            success = await _context.SaveChangesAsync();
            return success > 0;
        }

        // GET SHARED CALENDAR BY USER ID 
        public async Task<List<SharedCalendarDTO_GET>> GetSharedCalendarByUserId(Guid userId)
        {
            var data = await (from sc in _context.SharedCalendars
                        join ur in _context.UserAssignments on sc.SharedUserId equals ur.UserId
                        join c in _context.Calendars on sc.CalendarId equals c.CalendarId
                        join u in _context.Users
                        on sc.SharedUserId equals u.UserId
                        where sc.SharedUserId == userId
                        orderby sc.DateTimeCreated descending
                        select new SharedCalendarDTO_GET
                        {
                            CalendarOwnerId = c.UserId,
                            SharedCalendarId = sc.SharedCalendarId,
                            SharedUserId = ur.UserId,
                            CalendarId = c.CalendarId,
                            CalendarName = c.Name,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email
                            //DateTimeCreated = c.DateTimeCreated,
                        }).ToListAsync();
            return data;
        }
    }
}

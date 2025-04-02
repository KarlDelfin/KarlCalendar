using Calendar.Api.DatabaseConnection;
using Calendar.Api.DTO;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api.Logics
{
    public class CalendarLogic
    {
        private readonly CalendarContext _context;
        public CalendarLogic(CalendarContext context)
        {
            _context = context;
        }

        public async Task<List<CalendarDTO_GET>> GetCalendarByUserId(Guid userId, string? search = "")
        {
            var data = await (from c in _context.Calendars
                              where c.UserId == userId && c.IsDeleted == false
                              orderby c.DateTimeCreated descending
                              select new CalendarDTO_GET
                              {
                                  CalendarId = c.CalendarId,
                                  CalendarName = c.Name,
                                  DateTimeCreated = c.DateTimeCreated
                              }).ToListAsync();
            if (!string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(x => x.CalendarName.Contains(search)).ToList();
                return data;
            }
            return data;
        }

        public async Task<bool> AddCalendar(CalendarDTO_POST dto)
        {
            int success = 0;

            var data = new Calendar.Api.Models.Calendar();
            data.CalendarId = Guid.NewGuid();
            data.UserId = dto.UserId;
            data.Name = dto.CalendarName;
            data.DateTimeCreated = DateTime.Now;
            data.IsDeleted = false;

            _context.Calendars.Add(data);
            success = await _context.SaveChangesAsync();

            return success > 0;
        }

        public async Task<bool> UpdateCalendar(Guid calendarId, CalendarDTO_PUT dto)
        {
            int success = 0;

            var data = await _context.Calendars.FirstOrDefaultAsync(x => x.CalendarId == calendarId);
            data.Name = dto.CalendarName;

            _context.Calendars.Update(data);
            success = await _context.SaveChangesAsync();

            return success > 0;
        }

        public async Task<bool> DeleteCalendar(Guid calendarId)
        {
            int success = 0;

            var data = await _context.Calendars.FirstOrDefaultAsync(x => x.CalendarId == calendarId);
            data.IsDeleted = true;

            _context.Calendars.Update(data);
            success = await _context.SaveChangesAsync();

            return success > 0;
        }
    }
}

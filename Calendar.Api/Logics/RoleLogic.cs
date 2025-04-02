using Calendar.Api.DatabaseConnection;

namespace Calendar.Api.Logics
{
    public class RoleLogic
    {
        private readonly CalendarContext _context;
        public RoleLogic(CalendarContext context)
        {
            _context = context;
        }
    }
}

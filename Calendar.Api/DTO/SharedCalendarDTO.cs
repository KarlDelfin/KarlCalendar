namespace Calendar.API.DTO
{
    public class SharedCalendarDTO_GET
    {
        public Guid CalendarOwnerId { get; set; }
        public Guid SharedCalendarId { get; set; }
        public Guid SharedUserId { get; set; }
        public Guid CalendarId { get; set; }
        public string CalendarName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class SharedCalendarDTO_POST
    {
        public Guid SharedUserId { get; set; }
        public Guid CalendarId { get; set; }

    }
}

namespace Calendar.Api.DTO
{
    public class CalendarDTO_GET
    {
        public Guid CalendarId { get; set; }
        public string CalendarName { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }

    public class CalendarDTO_POST
    {
        public Guid UserId { get; set; }
        public string CalendarName { get; set; }
    }

    public class CalendarDTO_PUT
    {
        public string CalendarName { get; set; }
    }
}

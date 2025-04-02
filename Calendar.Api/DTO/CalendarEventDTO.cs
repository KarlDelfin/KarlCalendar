namespace Calendar.Api.DTO
{
    public class CalendarEventDTO_GET
    {
        public Guid CalendarEventId { get; set; }
        public Guid CalendarId { get; set; }
        public string CalendarName { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventColor { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime DateTimeStarted { get; set; }
        public DateTime DateTimeEnded { get; set; }
        public Guid? CalendarEventGroupId { get; set; }
    }
    public class CalendarEventDTO_POST
    {
        public Guid CalendarId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventColor { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime DateTimeStarted { get; set; }
        public DateTime DateTimeEnded { get; set; }
        public Guid? CalendarEventGroupId { get; set; }
    }

    public class CalendarEventDTO_PUT
    {
        public DateTime DateTimeStarted { get; set; }
        public DateTime DateTimeEnded { get; set; }
    }
}

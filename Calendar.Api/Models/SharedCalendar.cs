using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api.Models
{
    [Keyless]
    [Table("SharedCalendar")]
    public partial class SharedCalendar
    {
        public Guid SharedCalendarId { get; set; }
        public Guid CalendarOwnerUserId { get; set; }
        public Guid SharedUserId { get; set; }
        public Guid CalendarId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }
    }
}

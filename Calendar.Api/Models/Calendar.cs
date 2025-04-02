using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api.Models
{
    [Table("Calendar")]
    public partial class Calendar
    {
        public Calendar()
        {
            CalendarEvents = new HashSet<CalendarEvent>();
        }

        [Key]
        public Guid CalendarId { get; set; }
        public Guid UserId { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Calendars")]
        public virtual User User { get; set; } = null!;
        [InverseProperty("Calendar")]
        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
    }
}

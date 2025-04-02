using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api.Models
{
    [Table("CalendarEvent")]
    public partial class CalendarEvent
    {
        [Key]
        public Guid CalendarEventId { get; set; }
        public Guid CalendarId { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [StringLength(50)]
        public string Color { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime DateTimeStarted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTimeEnded { get; set; }
        public bool IsRecurring { get; set; }
        public Guid? CalendarEventGroupId { get; set; }

        [ForeignKey("CalendarId")]
        [InverseProperty("CalendarEvents")]
        public virtual Calendar Calendar { get; set; } = null!;
    }
}

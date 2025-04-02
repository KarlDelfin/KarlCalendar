using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api.Models
{
    [Table("Assignment")]
    public partial class Assignment
    {
        [Key]
        public Guid AssignmentId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }
        public bool IsDisabled { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("Assignments")]
        public virtual Role Role { get; set; } = null!;
        [ForeignKey("UserId")]
        [InverseProperty("Assignments")]
        public virtual User User { get; set; } = null!;
    }
}

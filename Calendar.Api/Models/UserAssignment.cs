using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api.Models
{
    [Table("UserAssignment")]
    public partial class UserAssignment
    {
        [Key]
        public Guid UserAssignmentId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTimeCreated { get; set; }
        public bool IsDisabled { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("UserAssignments")]
        public virtual Role Role { get; set; } = null!;
        [ForeignKey("UserId")]
        [InverseProperty("UserAssignments")]
        public virtual User User { get; set; } = null!;
    }
}

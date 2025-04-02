using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api.Models
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            UserAssignments = new HashSet<UserAssignment>();
        }

        [Key]
        public Guid RoleId { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public int RoleLevel { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<UserAssignment> UserAssignments { get; set; }
    }
}

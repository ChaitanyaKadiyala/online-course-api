using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourse.Core.Entities
{
    [Table("UserRole")]
    public partial class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("UserRoles")]
        public virtual Role Role { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserProfile.UserRoles))]
        public virtual UserProfile User { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourse.Core.Entities
{
    [Table("UserProfile")]
    public partial class UserProfile
    {
        public UserProfile()
        {
            Enrollments = new HashSet<Enrollment>();
            Instructors = new HashSet<Instructor>();
            Reviews = new HashSet<Review>();
            UserRoles = new HashSet<UserRole>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(100)]
        public string DisplayName { get; set; } = null!;
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [StringLength(128)]
        public string AdObjId { get; set; } = null!;

        [InverseProperty(nameof(Enrollment.User))]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [InverseProperty(nameof(Instructor.User))]
        public virtual ICollection<Instructor> Instructors { get; set; }
        [InverseProperty(nameof(Review.User))]
        public virtual ICollection<Review> Reviews { get; set; }
        [InverseProperty(nameof(UserRole.User))]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

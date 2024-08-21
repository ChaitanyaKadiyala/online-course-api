using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourse.Core.Entities
{
    [Table("Enrollment")]
    public partial class Enrollment
    {
        public Enrollment()
        {
            Payments = new HashSet<Payment>();
        }

        [Key]
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EnrollmentDate { get; set; }
        [StringLength(20)]
        public string PaymentStatus { get; set; } = null!;

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Enrollments")]
        public virtual Course Course { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserProfile.Enrollments))]
        public virtual UserProfile User { get; set; } = null!;
        [InverseProperty(nameof(Payment.Enrollment))]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

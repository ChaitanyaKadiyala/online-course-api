using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourse.Core.Entities
{
    [Table("Course")]
    public partial class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
            Reviews = new HashSet<Review>();
            SessionDetails = new HashSet<SessionDetail>();
        }

        [Key]
        public int CourseId { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [StringLength(10)]
        public string CourseType { get; set; } = null!;
        public int? SeatsAvailable { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Duration { get; set; }
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(CourseCategory.Courses))]
        public virtual CourseCategory Category { get; set; } = null!;
        [ForeignKey(nameof(InstructorId))]
        [InverseProperty("Courses")]
        public virtual Instructor Instructor { get; set; } = null!;
        [InverseProperty(nameof(Enrollment.Course))]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [InverseProperty(nameof(Review.Course))]
        public virtual ICollection<Review> Reviews { get; set; }
        [InverseProperty(nameof(SessionDetail.Course))]
        public virtual ICollection<SessionDetail> SessionDetails { get; set; }
    }
}

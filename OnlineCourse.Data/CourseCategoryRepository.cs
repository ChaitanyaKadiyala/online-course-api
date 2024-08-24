using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data
{
    public class CourseCategoryRepository : ICourseCategoryRepository
    {
        private readonly OnlineCourseDBContext dBContext;

        public CourseCategoryRepository(OnlineCourseDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public Task<List<CourseCategory>> GetCourseCategoriesAsync()
        {
            var result = this.dBContext.CourseCategories.ToListAsync();
            return result;
        }

        public Task<CourseCategory?> GetByIdAsync(int id)
        {
            var result = this.dBContext.CourseCategories.FindAsync(id).AsTask();
            return result;

        }
    }
}

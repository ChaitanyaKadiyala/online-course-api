using OnlineCourse.Core.Models;
using OnlineCourse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Service
{
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository categoryRepository;

        public CourseCategoryService(ICourseCategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<CourseCategoryModel?> GetByIdAsync(int id)
        {
            var result = await this.categoryRepository.GetByIdAsync(id);
            var model = result == null ? null : new CourseCategoryModel
            {
                CategoryId = result.CategoryId,
                Description = result.Description,
                CategoryName = result.CategoryName,
            };

            return model;
        }

        public async Task<List<CourseCategoryModel>> GetCourseCategoriesAsync()
        {
            var result = await this.categoryRepository.GetCourseCategoriesAsync();

            var model = result.Select(c => new CourseCategoryModel
            {
                CategoryId = c.CategoryId,
                Description = c.Description,
                CategoryName = c.CategoryName,

            }).ToList();

            return model;
        }
    }
}

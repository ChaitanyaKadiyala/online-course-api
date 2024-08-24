using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core.Entities;
using OnlineCourse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly OnlineCourseDBContext dBContext;

        public CourseRepository(OnlineCourseDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null)
        {
            //we first build a query to dynamically apply categoryid filter if it was sent
            // remember, this query is called deffered execution, it wont run untill we await or use sync methods like .ToList(), etc
            var query = dBContext.Courses
                .Include(c => c.Category)
                .AsQueryable();

            //Apply the filter if categoryId is provided
            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }

            var courses = await query
                .Select(s => new CourseModel
                {
                    CourseId = s.CourseId,
                    Title = s.Title,
                    Description = s.Description,
                    Price = s.Price,
                    CourseType = s.CourseType,
                    SeatsAvailable = s.SeatsAvailable,
                    Duration = s.Duration,
                    CategoryId = s.CategoryId,
                    InstructorId = s.InstructorId,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Category = new CourseCategoryModel
                    {
                        CategoryId = s.Category.CategoryId,
                        CategoryName = s.Category.CategoryName,
                        Description = s.Category.Description
                    },
                    UserRating = new UserRatingModel
                    {
                        CourseId = s.CourseId,
                        AverageRating = s.Reviews.Any() ? Convert.ToDecimal(s.Reviews.Average(r => r.Rating)) : 0,
                        TotalRating = s.Reviews.Count
                    }
                })
                .ToListAsync();

            return courses;
        }


        public async Task<CourseDetailModel> GetCourseDetailAsync(int courseId)
        {
            var course = await dBContext.Courses
                .Include(x => x.Category)
                .Include(x => x.Reviews)
                .Include(x => x.SessionDetails)
                .Where(x => x.CourseId == courseId)
                .Select(c => new CourseDetailModel
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    CourseType = c.CourseType,
                    SeatsAvailable = c.SeatsAvailable,
                    Duration = c.Duration,
                    CategoryId = c.CategoryId,
                    InstructorId = c.InstructorId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Category = new CourseCategoryModel()
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.CategoryName,
                        Description = c.Category.Description
                    },
                    Reviews = c.Reviews.Select(r => new UserReviewModel
                    {
                        CourseId = r.CourseId,
                        UserName = r.User.DisplayName,
                        Rating = r.Rating,
                        Comments = r.Comments,
                        ReviewDate = r.ReviewDate
                    }).OrderByDescending(o => o.Rating).Take(10).ToList(),
                    //  want to sort and take only top 10
                    SessionDetails = c.SessionDetails.Select(sd => new SessionDetailModel
                    {
                        SessionId = sd.SessionId,
                        CourseId = sd.CourseId,
                        Title = sd.Title,
                        Description = sd.Description,
                        VideoUrl = sd.VideoUrl,
                        VideoOrder = sd.VideoOrder
                    }).OrderBy(o => o.VideoOrder).ToList()//order the course sesisons by its proper order
                    ,
                    UserRating = new UserRatingModel
                    {
                        CourseId = c.CourseId,
                        AverageRating = c.Reviews.Any() ? Convert.ToDecimal(c.Reviews.Average(r => r.Rating)) : 0,
                        TotalRating = c.Reviews.Count
                    }

                }).FirstOrDefaultAsync();

            return course;
        }
    }
}

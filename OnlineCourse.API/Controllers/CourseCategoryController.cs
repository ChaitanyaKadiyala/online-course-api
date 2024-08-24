using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Service;

namespace OnlineCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly ICourseCategoryService courseCategoryService;

        public CourseCategoryController(ICourseCategoryService courseCategoryService)
        {
            this.courseCategoryService = courseCategoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await this.courseCategoryService.GetByIdAsync(id);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);


        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await this.courseCategoryService.GetCourseCategoriesAsync();
            return Ok(categories);
        }
    }
}

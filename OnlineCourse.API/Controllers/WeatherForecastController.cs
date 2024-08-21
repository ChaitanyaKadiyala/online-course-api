using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Core.Entities;

namespace OnlineCourse.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly OnlineCourseDBContext dBContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, OnlineCourseDBContext dBContext)
        {
            _logger = logger;
            this.dBContext = dBContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var courses = this.dBContext.Courses.ToList();
            return Ok(courses);
        }
    }
}
using OnlineCourse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Data;
using OnlineCourse.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<OnlineCourseDBContext>(options => 
    { 
     options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineCourseDB"),
     provideroptions => provideroptions.EnableRetryOnFailure());

     // shows the db query with parameter values
     //options.EnableSensitiveDataLogging();
    
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
builder.Services.AddScoped<ICourseCategoryService, CourseCategoryService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

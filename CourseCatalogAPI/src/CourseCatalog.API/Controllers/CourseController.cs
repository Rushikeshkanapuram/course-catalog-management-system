using CourseCatalog.Application.DTOs.Course;
using CourseCatalog.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CourseSearchDto searchDto)
    {
        if (string.IsNullOrWhiteSpace(searchDto.Title)
            && string.IsNullOrWhiteSpace(searchDto.Category)
            && !searchDto.InstructorId.HasValue
            && !searchDto.StartDate.HasValue)
        {
            var courses = await _courseService.GetAllCoursesAsync();

            return Ok(courses);
        }

        var coursesFiltered = await _courseService.SearchAsync(searchDto);

        return Ok(coursesFiltered);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);

        if (course == null)
            return NotFound();

        return Ok(course);
    }

    [Authorize(Roles = "Admin,Instructor")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseDto dto)
    {
        var currentUserId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var role = User.FindFirstValue(ClaimTypes.Role)!;

        await _courseService.CreateCourseAsync(
            dto,
            currentUserId,
            role);

        return Ok(new
        {
            message = "Course created successfully."
        });
    }

    [Authorize(Roles = "Admin,Instructor")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCourseDto dto)
    {
        var currentUserId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var role = User.FindFirstValue(ClaimTypes.Role)!;

        await _courseService.UpdateCourseAsync(
            id,
            dto,
            currentUserId,
            role);

        return Ok(new
        {
            message = "Course updated successfully."
        });
    }

    [Authorize(Roles = "Admin,Instructor")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var currentUserId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var role = User.FindFirstValue(ClaimTypes.Role)!;

        await _courseService.DeleteCourseAsync(
            id,
            currentUserId,
            role);
        return Ok(new
        {
            message = "Course deleted successfully."
        });
    }
}
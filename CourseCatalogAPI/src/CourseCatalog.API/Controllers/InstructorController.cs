using System.Security.Claims;
using CourseCatalog.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Instructor")]
public class InstructorController : ControllerBase
{
    private readonly IInstructorService _instructorService;

    public InstructorController(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }

    [HttpGet("my-courses")]
    public async Task<IActionResult> GetMyCourses()
    {
        var instructorId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var courses = await _instructorService
            .GetMyCoursesAsync(instructorId);

        return Ok(courses);
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetAllStudents()
    {
        var instructorId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var students = await _instructorService
            .GetAllStudentsAsync(instructorId);

        return Ok(students);
    }

    [HttpGet("course/{courseId:int}/students")]
    public async Task<IActionResult> GetCourseStudents(int courseId)
    {
        var instructorId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var students = await _instructorService
            .GetCourseStudentsAsync(courseId, instructorId);

        return Ok(students);
    }
}
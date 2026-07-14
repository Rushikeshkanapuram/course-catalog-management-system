using System.Security.Claims;
using CourseCatalog.Application.DTOs.Enrollment;
using CourseCatalog.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Student")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<IActionResult> Enroll(CreateEnrollmentDto dto)
    {
        var studentId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _enrollmentService.EnrollAsync(studentId, dto);

        return Ok(new
        {
            Message = "Enrollment successful."
        });
    }

    [HttpGet("my-enrollments")]
    public async Task<IActionResult> GetMyEnrollments()
    {
        var studentId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var enrollments = await _enrollmentService
            .GetMyEnrollmentsAsync(studentId);

        return Ok(enrollments);
    }

    [HttpDelete("{enrollmentId}")]
    public async Task<IActionResult> DropCourse(int enrollmentId)
    {
        var studentId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _enrollmentService.DropCourseAsync(enrollmentId, studentId);

        return Ok(new
        {
            Message = "Course dropped successfully."
        });
    }

}
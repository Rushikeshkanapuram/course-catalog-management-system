using CourseCatalog.Application.Interfaces.Services;
using CourseCatalog.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;

using System.Security.Claims;
using CourseCatalog.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseCatalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAdminDashboard()
    {
        var dashboard = await _dashboardService.GetAdminDashboardAsync();

        return Ok(dashboard);
    }

    [HttpGet("instructor")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> GetInstructorDashboard()
    {
        var instructorId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var dashboard = await _dashboardService
            .GetInstructorDashboardAsync(instructorId);

        return Ok(dashboard);
    }

    [HttpGet("student")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> GetStudentDashboard()
    {
        var studentId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var dashboard = await _dashboardService
            .GetStudentDashboardAsync(studentId);

        return Ok(dashboard);
    }
}
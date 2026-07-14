using CourseCatalog.Application.DTOs.User;
using CourseCatalog.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }

    [HttpPost("instructor")]
    public async Task<IActionResult> CreateInstructor(CreateInstructorDto dto)
    {
        await _userService.CreateInstructorAsync(dto);

        return Ok(new
        {
            message = "Instructor created successfully."
        });
    }
}
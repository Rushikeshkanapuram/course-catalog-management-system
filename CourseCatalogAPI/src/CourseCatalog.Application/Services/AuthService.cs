using CourseCatalog.Application.DTOs.Auth;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Application.Interfaces.Repositories;
using CourseCatalog.Application.Interfaces.Services;
using CourseCatalog.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CourseCatalog.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            throw new Exception("Invalid email or password.");

        var passwordHasher = new PasswordHasher<User>();

        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid email or password.");

        var token = _jwtTokenService.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            Role = user.Role.Name,
            FullName = $"{user.FirstName} {user.LastName}"
        };
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var emailExists = await _userRepository.EmailExistsAsync(request.Email);

        if (emailExists)
            throw new BadRequestException("Email is already registered.");

        var studentRole = await _userRepository.GetRoleByNameAsync("Student");

        if (studentRole == null)
            throw new Exception("Student role not found.");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            RoleId = studentRole.Id
        };

        var passwordHasher = new PasswordHasher<User>();

        user.PasswordHash = passwordHasher.HashPassword(
            user,
            request.Password);

        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();
    }

    public async Task<CurrentUserDto> GetCurrentUserAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new NotFoundException("User not found.");

        return new CurrentUserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role.Name
        };
    }
}
using CourseCatalog.Application.DTOs.User;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Application.Interfaces.Repositories;
using CourseCatalog.Application.Interfaces.Services;
using CourseCatalog.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CourseCatalog.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(u => new UserDto
        {
            Id = u.Id,
            FullName = $"{u.FirstName} {u.LastName}",
            Email = u.Email,
            Role = u.Role.Name
        });
    }


    public async Task CreateInstructorAsync(CreateInstructorDto dto)
    {
        var emailExists = await _userRepository.EmailExistsAsync(dto.Email);

        if (emailExists)
            throw new BadRequestException("Email is already registered.");

        var instructorRole = await _userRepository.GetRoleByNameAsync("Instructor");

        if (instructorRole == null)
            throw new NotFoundException("Instructor role not found.");

        var instructor = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            RoleId = instructorRole.Id
        };

        var passwordHasher = new PasswordHasher<User>();

        instructor.PasswordHash = passwordHasher.HashPassword(
            instructor,
            dto.Password);

        await _userRepository.AddAsync(instructor);

        await _userRepository.SaveChangesAsync();
    }
}
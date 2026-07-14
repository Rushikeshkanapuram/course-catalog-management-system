using CourseCatalog.Application.DTOs.User;

namespace CourseCatalog.Application.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();

    Task CreateInstructorAsync(CreateInstructorDto dto);
}
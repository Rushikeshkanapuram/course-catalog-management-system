using CourseCatalog.Application.DTOs.Auth;

namespace CourseCatalog.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);

    Task RegisterAsync(RegisterRequestDto request);

    Task<CurrentUserDto> GetCurrentUserAsync(int userId);
}
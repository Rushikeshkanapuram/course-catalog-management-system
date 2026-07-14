using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
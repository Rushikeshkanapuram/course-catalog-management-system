using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<bool> EmailExistsAsync(string email);

    Task AddAsync(User user);

    Task SaveChangesAsync();

    Task<Role?> GetRoleByNameAsync(string roleName);

    Task<User?> GetByIdAsync(int id);

    Task<IEnumerable<User>> GetAllAsync();

    Task<int> GetTotalUsersAsync();

    Task<int> GetTotalStudentsAsync();

    Task<int> GetTotalInstructorsAsync();
}
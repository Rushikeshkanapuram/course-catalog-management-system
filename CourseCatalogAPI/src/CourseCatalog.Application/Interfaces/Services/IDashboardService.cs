using CourseCatalog.Application.DTOs.Dashboard;

namespace CourseCatalog.Application.Interfaces.Services;

public interface IDashboardService
{
    Task<AdminDashboardDto> GetAdminDashboardAsync();

    Task<InstructorDashboardDto> GetInstructorDashboardAsync(int instructorId);

    Task<StudentDashboardDto> GetStudentDashboardAsync(int studentId);
}
using CourseCatalog.Application.DTOs.Enrollment;

namespace CourseCatalog.Application.Interfaces.Services;

public interface IEnrollmentService
{
    Task EnrollAsync(int studentId, CreateEnrollmentDto dto);

    Task<IEnumerable<EnrollmentDto>> GetMyEnrollmentsAsync(int studentId);

    Task DropCourseAsync(int enrollmentId, int studentId);
}
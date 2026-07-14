using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<bool> IsAlreadyEnrolledAsync(int studentId, int courseId);

    Task<int> GetEnrollmentCountAsync(int courseId);

    Task AddAsync(Enrollment enrollment);

    Task<IEnumerable<Enrollment>> GetStudentEnrollmentsAsync(int studentId);

    Task<Enrollment?> GetByIdAsync(int enrollmentId);

    Task SaveChangesAsync();

    void Update(Enrollment enrollment);

    Task<IEnumerable<Enrollment>> GetCourseStudentsAsync(int courseId);

    Task<int> GetTotalEnrollmentsAsync();

    Task<int> GetStudentEnrollmentCountAsync(int studentId);

    Task<int> GetInstructorStudentCountAsync(int instructorId);

    Task<int> GetInstructorEnrollmentCountAsync(int instructorId);

    Task<Enrollment?> GetEnrollmentAsync(int studentId, int courseId);
}
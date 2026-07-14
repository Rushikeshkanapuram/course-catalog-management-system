using CourseCatalog.Application.Interfaces.Repositories;
using CourseCatalog.Domain.Entities;
using CourseCatalog.Domain.Enums;
using CourseCatalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context;

    public EnrollmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsAlreadyEnrolledAsync(int studentId, int courseId)
    {
        return await _context.Enrollments
            .AnyAsync(e =>
                e.StudentId == studentId &&
                e.CourseId == courseId &&
                e.Status == EnrollmentStatus.Active);
    }

    public async Task<int> GetEnrollmentCountAsync(int courseId)
    {
        return await _context.Enrollments
            .CountAsync(e =>
                e.CourseId == courseId &&
                e.Status == EnrollmentStatus.Active);
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
    }

    public async Task<IEnumerable<Enrollment>> GetStudentEnrollmentsAsync(int studentId)
    {
        return await _context.Enrollments
            .Include(e => e.Course)
            .Where(e =>
                e.StudentId == studentId &&
                e.Status == EnrollmentStatus.Active)
            .ToListAsync();
    }

    public async Task<Enrollment?> GetByIdAsync(int enrollmentId)
    {
        return await _context.Enrollments
            .FirstOrDefaultAsync(e => e.Id == enrollmentId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
    }

    public async Task<IEnumerable<Enrollment>> GetCourseStudentsAsync(int courseId)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Where(e => e.CourseId == courseId &&
                        e.Status == EnrollmentStatus.Active)
            .ToListAsync();
    }

    public async Task<int> GetTotalEnrollmentsAsync()
    {
        return await _context.Enrollments
            .CountAsync(e => e.Status == EnrollmentStatus.Active);
    }

    public async Task<int> GetStudentEnrollmentCountAsync(int studentId)
    {
        return await _context.Enrollments
            .CountAsync(e =>
                e.StudentId == studentId &&
                e.Status == EnrollmentStatus.Active);
    }

    public async Task<int> GetInstructorStudentCountAsync(int instructorId)
    {
        return await _context.Enrollments
            .Where(e =>
                e.Course.InstructorId == instructorId &&
                e.Status == EnrollmentStatus.Active)
            .Select(e => e.StudentId)
            .Distinct()
            .CountAsync();
    }

    public async Task<int> GetInstructorEnrollmentCountAsync(int instructorId)
    {
        return await _context.Enrollments
            .CountAsync(e =>
                e.Course.InstructorId == instructorId &&
                e.Status == EnrollmentStatus.Active);
    }
    public async Task<Enrollment?> GetEnrollmentAsync(
    int studentId,
    int courseId)
    {
        return await _context.Enrollments
            .FirstOrDefaultAsync(e =>
                e.StudentId == studentId &&
                e.CourseId == courseId);
    }
}

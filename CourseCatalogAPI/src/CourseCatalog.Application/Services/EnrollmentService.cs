using AutoMapper;
using CourseCatalog.Application.DTOs.Enrollment;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Application.Interfaces.Repositories;
using CourseCatalog.Application.Interfaces.Services;
using CourseCatalog.Domain.Entities;
using CourseCatalog.Domain.Enums;


namespace CourseCatalog.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public EnrollmentService(
        IEnrollmentRepository enrollmentRepository,
        ICourseRepository courseRepository,
        IMapper mapper)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task EnrollAsync(int studentId, CreateEnrollmentDto dto)
    {
        var course = await _courseRepository.GetByIdAsync(dto.CourseId);

        if (course == null)
            throw new NotFoundException("Course not found.");

        var existingEnrollment = await _enrollmentRepository
            .GetEnrollmentAsync(studentId, dto.CourseId);

        if (existingEnrollment != null)
        {
            if (existingEnrollment.Status == EnrollmentStatus.Active)
            {
                throw new BadRequestException(
                    "Student is already enrolled in this course.");
            }

            // Reactivate the dropped enrollment
            existingEnrollment.Status = EnrollmentStatus.Active;
            existingEnrollment.EnrollmentDate = DateTime.UtcNow;

            _enrollmentRepository.Update(existingEnrollment);
            await _enrollmentRepository.SaveChangesAsync();

            return;
        }

        var enrollmentCount = await _enrollmentRepository
            .GetEnrollmentCountAsync(dto.CourseId);

        if (enrollmentCount >= course.Capacity)
            throw new BadRequestException("Course is full.");

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = dto.CourseId,
            EnrollmentDate = DateTime.UtcNow,
            Status = EnrollmentStatus.Active
        };

        await _enrollmentRepository.AddAsync(enrollment);

        await _enrollmentRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<EnrollmentDto>> GetMyEnrollmentsAsync(int studentId)
    {
        var enrollments = await _enrollmentRepository
            .GetStudentEnrollmentsAsync(studentId);

        return enrollments.Select(e => new EnrollmentDto
        {
            Id = e.Id,
            CourseId = e.CourseId,
            CourseTitle = e.Course.Title,
            EnrollmentDate = e.EnrollmentDate
        });
    }



    public async Task DropCourseAsync(int enrollmentId, int studentId)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);

        if (enrollment == null)
            throw new NotFoundException("Enrollment not found.");

        if (enrollment.StudentId != studentId)
            throw new BadRequestException("You cannot drop another student's enrollment.");

        if (enrollment.Status == EnrollmentStatus.Dropped)
            throw new BadRequestException("Enrollment is already dropped.");

        enrollment.Status = EnrollmentStatus.Dropped;
        _enrollmentRepository.Update(enrollment);

        await _enrollmentRepository.SaveChangesAsync();
    }
}
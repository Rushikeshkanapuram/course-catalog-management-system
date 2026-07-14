using AutoMapper;
using CourseCatalog.Application.DTOs.Course;
using CourseCatalog.Application.DTOs.Enrollment;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Application.Interfaces.Repositories;
using CourseCatalog.Application.Interfaces.Services;
using CourseCatalog.Application.DTOs.Instructor;
using CourseCatalog.Domain.Enums;
namespace CourseCatalog.Application.Services;

public class InstructorService : IInstructorService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public InstructorService(
     ICourseRepository courseRepository,
     IEnrollmentRepository enrollmentRepository,
     IMapper mapper)
    {
        _courseRepository = courseRepository;
        _enrollmentRepository = enrollmentRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CourseDto>> GetMyCoursesAsync(int instructorId)
    {
        var courses = await _courseRepository
            .GetInstructorCoursesAsync(instructorId);

        return _mapper.Map<IEnumerable<CourseDto>>(courses);
    }

    public async Task<IEnumerable<CourseStudentDto>> GetCourseStudentsAsync(
    int courseId,
    int instructorId)
    {
        // Check course exists
        var course = await _courseRepository.GetByIdAsync(courseId);

        if (course == null)
            throw new NotFoundException("Course not found.");

        // Instructor can view only his own courses
        if (course.InstructorId != instructorId)
            throw new UnauthorizedAccessException(
                "You can view students only for your own courses.");

        var enrollments = await _enrollmentRepository
            .GetCourseStudentsAsync(courseId);

        return enrollments.Select(e => new CourseStudentDto
        {
            StudentId = e.StudentId,
            FullName = $"{e.Student.FirstName} {e.Student.LastName}",
            Email = e.Student.Email,
            EnrollmentDate = e.EnrollmentDate
        });
    }

    public async Task<IEnumerable<InstructorStudentsDto>> GetAllStudentsAsync(int instructorId)
    {
        var courses = await _courseRepository
            .GetInstructorCoursesWithStudentsAsync(instructorId);

        return courses.Select(course => new InstructorStudentsDto
        {
            CourseId = course.Id,

            CourseTitle = course.Title,

            Students = course.Enrollments
                .Where(e => e.Status == EnrollmentStatus.Active)
                .Select(e => new StudentInfoDto
                {
                    StudentId = e.StudentId,

                    FullName = $"{e.Student.FirstName} {e.Student.LastName}",

                    Email = e.Student.Email,

                    EnrollmentDate = e.EnrollmentDate
                })
                .ToList()
        });
    }
}
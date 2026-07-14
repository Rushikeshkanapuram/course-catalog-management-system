using CourseCatalog.Application.DTOs.Course;
using CourseCatalog.Application.DTOs.Enrollment;
using CourseCatalog.Application.DTOs.Instructor;
namespace CourseCatalog.Application.Interfaces.Services;

public interface IInstructorService
{
    Task<IEnumerable<CourseDto>> GetMyCoursesAsync(int instructorId);

    Task<IEnumerable<CourseStudentDto>> GetCourseStudentsAsync(
    int courseId,
    int instructorId);

    Task<IEnumerable<InstructorStudentsDto>> GetAllStudentsAsync(int instructorId);
}
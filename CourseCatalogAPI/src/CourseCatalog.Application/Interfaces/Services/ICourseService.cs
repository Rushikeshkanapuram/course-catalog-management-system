using CourseCatalog.Application.DTOs.Course;

namespace CourseCatalog.Application.Interfaces.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllCoursesAsync();

    Task<CourseDto?> GetCourseByIdAsync(int id);

    Task CreateCourseAsync(
    CreateCourseDto dto,
    int currentUserId,
    string role);

    Task UpdateCourseAsync(
     int id,
     UpdateCourseDto dto,
     int currentUserId,
     string role);

    Task DeleteCourseAsync(
        int id,
        int currentUserId,
        string role);

    Task<IEnumerable<CourseDto>> SearchAsync(CourseSearchDto searchDto);
}
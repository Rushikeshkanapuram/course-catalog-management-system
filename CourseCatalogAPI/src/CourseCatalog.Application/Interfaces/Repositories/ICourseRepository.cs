using CourseCatalog.Application.DTOs.Course;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Interfaces.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();

    Task<Course?> GetByIdAsync(int id);

    Task AddAsync(Course course);

    void Update(Course course);

    void Delete(Course course);

    Task SaveChangesAsync();

    Task<bool> CourseCodeExistsAsync(string code);

    Task<bool> InstructorExistsAsync(int instructorId);

    Task<bool> CourseCodeExistsAsync(string code, int excludeCourseId);

    Task<IEnumerable<Course>> SearchAsync(CourseSearchDto searchDto);

    Task<IEnumerable<Course>> GetInstructorCoursesAsync(int instructorId);

    Task<int> GetTotalCoursesAsync();

    Task<int> GetInstructorCourseCountAsync(int instructorId);

    Task<IEnumerable<Course>> GetInstructorCoursesWithStudentsAsync(int instructorId);

}
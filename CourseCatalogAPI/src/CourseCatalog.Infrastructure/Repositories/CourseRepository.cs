using CourseCatalog.Application.DTOs.Course;
using CourseCatalog.Application.Interfaces.Repositories;
using CourseCatalog.Domain.Entities;
using CourseCatalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .Where(c => !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
    }

    public async Task AddAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
    }

    public void Update(Course course)
    {
        _context.Courses.Update(course);
    }

    public void Delete(Course course)
    {
        course.IsDeleted = true;

        _context.Courses.Update(course);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CourseCodeExistsAsync(string code)
    {
        return await _context.Courses
            .AnyAsync(c => c.Code == code && !c.IsDeleted);
    }

    public async Task<bool> InstructorExistsAsync(int instructorId)
    {
        return await _context.Users
            .AnyAsync(u => u.Id == instructorId);
    }

    public async Task<bool> CourseCodeExistsAsync(string code, int excludeCourseId)
    {
        return await _context.Courses
            .AnyAsync(c =>
                c.Code == code &&
                c.Id != excludeCourseId &&
                !c.IsDeleted);
    }

    public async Task<IEnumerable<Course>> SearchAsync(CourseSearchDto searchDto)
    {
        IQueryable<Course> query = _context.Courses
            .Include(c => c.Instructor)
            .Where(c => !c.IsDeleted);

        if (!string.IsNullOrWhiteSpace(searchDto.Title))
        {
            query = query.Where(c =>
                c.Title.Contains(searchDto.Title));
        }

        if (!string.IsNullOrWhiteSpace(searchDto.Category))
        {
            query = query.Where(c =>
                c.Category == searchDto.Category);
        }

        if (searchDto.InstructorId.HasValue)
        {
            query = query.Where(c =>
                c.InstructorId == searchDto.InstructorId.Value);
        }

        if (searchDto.StartDate.HasValue)
        {
            query = query.Where(c =>
                c.StartDate >= searchDto.StartDate.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetInstructorCoursesAsync(int instructorId)
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .Where(c => c.InstructorId == instructorId &&
                        !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<int> GetTotalCoursesAsync()
    {
        return await _context.Courses
            .CountAsync(c => !c.IsDeleted);
    }

    public async Task<int> GetInstructorCourseCountAsync(int instructorId)
    {
        return await _context.Courses
            .CountAsync(c =>
                c.InstructorId == instructorId &&
                !c.IsDeleted);
    }
    public async Task<IEnumerable<Course>> GetInstructorCoursesWithStudentsAsync(int instructorId)
    {
        return await _context.Courses

            .Include(c => c.Enrollments)

                .ThenInclude(e => e.Student)

            .Where(c =>
                c.InstructorId == instructorId &&
                !c.IsDeleted)

            .ToListAsync();
    }
}
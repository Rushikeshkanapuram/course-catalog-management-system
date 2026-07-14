using AutoMapper;
using CourseCatalog.Application.DTOs.Course;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Application.Interfaces.Repositories;
using CourseCatalog.Application.Interfaces.Services;
using CourseCatalog.Domain.Entities;
using System.Data;

namespace CourseCatalog.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CourseService(
        ICourseRepository courseRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<CourseDto>>(courses);
    }

    public async Task<CourseDto?> GetCourseByIdAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);

        if (course == null)
            return null;

        return _mapper.Map<CourseDto>(course);
    }

    public async Task CreateCourseAsync(
    CreateCourseDto dto,
    int currentUserId,
    string role)
    {
        if (role != "Admin")
        {
            dto.InstructorId = currentUserId;
        }
        // Validate course code
        if (await _courseRepository.CourseCodeExistsAsync(dto.Code))
        {
            throw new Exception("Course code already exists.");
        }

        // Validate instructor
        if (!await _courseRepository.InstructorExistsAsync(dto.InstructorId))
        {
            throw new Exception("Instructor not found.");
        }

        // Validate dates
        if (dto.StartDate >= dto.EndDate)
        {
            throw new Exception("Start date must be before end date.");
        }

        // Validate capacity
        if (dto.Capacity <= 0)
        {
            throw new Exception("Capacity must be greater than zero.");
        }

        var course = _mapper.Map<Course>(dto);

        await _courseRepository.AddAsync(course);

        await _courseRepository.SaveChangesAsync();
    }

    public async Task UpdateCourseAsync(
        int id,
        UpdateCourseDto dto,
        int currentUserId,
        string role)
    {
        if (role != "Admin")
        {
            dto.InstructorId = currentUserId;
        }

        var course = await _courseRepository.GetByIdAsync(id);

        if (course == null)
            throw new NotFoundException("Course not found.");

        if (role != "Admin" &&
    course.InstructorId != currentUserId)
        {
            throw new UnauthorizedAccessException(
                "You can update only your own courses.");
        }

        if (course == null)
            throw new Exception("Course not found.");

        if (await _courseRepository.CourseCodeExistsAsync(dto.Code, id))
            throw new Exception("Course code already exists.");

        if (!await _courseRepository.InstructorExistsAsync(dto.InstructorId))
            throw new Exception("Instructor not found.");

        if (dto.StartDate >= dto.EndDate)
            throw new Exception("Start date must be before end date.");

        if (dto.Capacity <= 0)
            throw new Exception("Capacity must be greater than zero.");

        _mapper.Map(dto, course);

        _courseRepository.Update(course);

        await _courseRepository.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(
        int id,
        int currentUserId,
        string role)
    {
        var course = await _courseRepository.GetByIdAsync(id);

        if (course == null)
            throw new NotFoundException("Course not found.");

        if (role != "Admin" &&
            course.InstructorId != currentUserId)
        {
            throw new UnauthorizedAccessException(
                "You can delete only your own courses.");
        }

        if (course == null)
            throw new NotFoundException("Course not found.");

        _courseRepository.Delete(course);

        await _courseRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<CourseDto>> SearchAsync(CourseSearchDto searchDto)
    {
        var courses = await _courseRepository.SearchAsync(searchDto);

        return _mapper.Map<IEnumerable<CourseDto>>(courses);
    }

}
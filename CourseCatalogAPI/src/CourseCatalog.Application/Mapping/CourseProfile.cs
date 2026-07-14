using AutoMapper;
using CourseCatalog.Application.DTOs.Course;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Mapping;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src =>
                    src.Instructor.FirstName + " " + src.Instructor.LastName));

        CreateMap<CreateCourseDto, Course>();

        CreateMap<UpdateCourseDto, Course>();
    }
}
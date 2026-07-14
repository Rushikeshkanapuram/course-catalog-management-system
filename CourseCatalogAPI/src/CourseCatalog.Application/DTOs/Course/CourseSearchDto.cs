namespace CourseCatalog.Application.DTOs.Course;

public class CourseSearchDto
{
    public string? Title { get; set; }

    public string? Category { get; set; }

    public int? InstructorId { get; set; }

    public DateTime? StartDate { get; set; }
}
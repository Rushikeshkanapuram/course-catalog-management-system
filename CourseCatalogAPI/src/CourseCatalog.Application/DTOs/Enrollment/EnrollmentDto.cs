namespace CourseCatalog.Application.DTOs.Enrollment;

public class EnrollmentDto
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string CourseTitle { get; set; } = string.Empty;

    public DateTime EnrollmentDate { get; set; }
}
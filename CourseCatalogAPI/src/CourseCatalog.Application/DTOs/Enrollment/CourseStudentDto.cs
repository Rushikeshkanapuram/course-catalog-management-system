namespace CourseCatalog.Application.DTOs.Enrollment;

public class CourseStudentDto
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime EnrollmentDate { get; set; }
}
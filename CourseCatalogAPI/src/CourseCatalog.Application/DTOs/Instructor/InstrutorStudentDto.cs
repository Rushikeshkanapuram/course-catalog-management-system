namespace CourseCatalog.Application.DTOs.Instructor;

public class InstructorStudentsDto
{
    public int CourseId { get; set; }

    public string CourseTitle { get; set; } = string.Empty;

    public List<StudentInfoDto> Students { get; set; } = [];
}

public class StudentInfoDto
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime EnrollmentDate { get; set; }
}
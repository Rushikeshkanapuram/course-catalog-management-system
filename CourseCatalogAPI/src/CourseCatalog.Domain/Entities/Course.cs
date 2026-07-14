using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int InstructorId { get; set; }

    public User Instructor { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Capacity { get; set; }

    public string Code { get; set; } = string.Empty;

    // Navigation Properties
    public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
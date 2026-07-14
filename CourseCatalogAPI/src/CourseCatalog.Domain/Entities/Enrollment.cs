using CourseCatalog.Domain.Common;
using CourseCatalog.Domain.Enums;

namespace CourseCatalog.Domain.Entities;

public class Enrollment : BaseEntity
{
    public int StudentId { get; set; }

    public User Student { get; set; } = null!;

    public int CourseId { get; set; }

    public Course Course { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;

    public ICollection<EnrollmentLog> EnrollmentLogs { get; set; } = new List<EnrollmentLog>();
}
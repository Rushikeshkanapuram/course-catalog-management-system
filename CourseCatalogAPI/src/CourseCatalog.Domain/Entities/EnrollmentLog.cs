using CourseCatalog.Domain.Common;
using CourseCatalog.Domain.Enums;

namespace CourseCatalog.Domain.Entities;

public class EnrollmentLog : BaseEntity
{
    public int EnrollmentId { get; set; }

    public Enrollment Enrollment { get; set; } = null!;

    public string Action { get; set; } = string.Empty;

    public DateTime ActionDate { get; set; } = DateTime.UtcNow;
}
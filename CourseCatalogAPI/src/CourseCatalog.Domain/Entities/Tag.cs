using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    // Navigation Property
    public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
}
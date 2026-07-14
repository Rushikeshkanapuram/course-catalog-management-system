using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Course> Courses => Set<Course>();

    public DbSet<Tag> Tags => Set<Tag>();

    public DbSet<CourseTag> CourseTags => Set<CourseTag>();

    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    public DbSet<EnrollmentLog> EnrollmentLogs => Set<EnrollmentLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Infrastructure.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.Code)
       .IsRequired()
       .HasMaxLength(20);

        builder.HasIndex(c => c.Code)
               .IsUnique();

        builder.Property(c => c.Description)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(c => c.Category)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Capacity)
               .IsRequired();

        builder.HasOne(c => c.Instructor)
               .WithMany(u => u.Courses)
               .HasForeignKey(c => c.InstructorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
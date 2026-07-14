using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Infrastructure.Configurations;

public class EnrollmentLogConfiguration : IEntityTypeConfiguration<EnrollmentLog>
{
    public void Configure(EntityTypeBuilder<EnrollmentLog> builder)
    {
        builder.ToTable("EnrollmentLogs");

        builder.HasKey(el => el.Id);

        builder.Property(el => el.Action)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(el => el.ActionDate)
               .IsRequired();

        builder.HasOne(el => el.Enrollment)
               .WithMany(e => e.EnrollmentLogs)
               .HasForeignKey(el => el.EnrollmentId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
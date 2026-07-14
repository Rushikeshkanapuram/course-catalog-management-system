using CourseCatalog.Infrastructure.Persistence;
using CourseCatalog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace CourseCatalog.Infrastructure.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (!await context.Roles.AnyAsync())
        {
            context.Roles.AddRange(
                new Role { Name = "Admin" },
                new Role { Name = "Instructor" },
                new Role { Name = "Student" }
            );

            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var passwordHasher = new PasswordHasher<User>();

            var adminRole = await context.Roles.FirstAsync(r => r.Name == "Admin");
            var instructorRole = await context.Roles.FirstAsync(r => r.Name == "Instructor");
            var studentRole = await context.Roles.FirstAsync(r => r.Name == "Student");

            var admin = new User
            {
                FirstName = "System",
                LastName = "Admin",
                Email = "admin@coursecatalog.com",
                RoleId = adminRole.Id
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin@123");

            var instructor = new User
            {
                FirstName = "John",
                LastName = "Instructor",
                Email = "instructor@coursecatalog.com",
                RoleId = instructorRole.Id
            };

            instructor.PasswordHash = passwordHasher.HashPassword(instructor, "Instructor@123");

            var student = new User
            {
                FirstName = "Alice",
                LastName = "Student",
                Email = "student@coursecatalog.com",
                RoleId = studentRole.Id
            };

            student.PasswordHash = passwordHasher.HashPassword(student, "Student@123");

            context.Users.AddRange(admin, instructor, student);

            await context.SaveChangesAsync();
        }
    }
}
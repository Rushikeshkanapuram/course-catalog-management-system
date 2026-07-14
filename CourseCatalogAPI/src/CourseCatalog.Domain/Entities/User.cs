using CourseCatalog.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseCatalog.Domain.Entities
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    }
}

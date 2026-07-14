using System;
using System.Collections.Generic;
using System.Text;

namespace CourseCatalog.Domain.Common
{
    public abstract  class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

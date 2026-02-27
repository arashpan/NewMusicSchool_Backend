using System.Collections.Generic;
using MusicSchool.Domain.Common;

namespace MusicSchool.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; } // پسورد رمزگذاری شده
        public string? FullName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } // ارجاع به نقش‌های کاربر
    }
}
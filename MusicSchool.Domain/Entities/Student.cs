using System;
using MusicSchool.Domain.Common;

namespace MusicSchool.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        // فعلاً فقط Id شعبه را نگه می‌داریم (برای جلوگیری از وابستگی به مدل Branch در این API)
        public int BranchId { get; set; }

        // اگر زیر 15 سال باشد، باید Parent داشته باشد (در API enforce می‌کنیم)
        public int? ParentId { get; set; }
        public Parent? Parent { get; set; }
    }
}
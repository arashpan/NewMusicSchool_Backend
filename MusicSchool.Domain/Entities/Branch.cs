using MusicSchool.Domain.Common;

namespace MusicSchool.Domain.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; } = string.Empty;  // نام شعبه
        public string Location { get; set; } = string.Empty;  // مکان شعبه
        public bool IsMainBranch { get; set; }  // برای مشخص کردن اینکه شعبه اصلی است یا نه
        public int? ParentBranchId { get; set; }  // برای ارتباط با شعبه اصلی، اگر این شعبه فرعی باشد
        public Branch? ParentBranch { get; set; }  // ارتباط با شعبه اصلی
        public ICollection<Branch> SubBranches { get; set; } = new List<Branch>();  // زیر شعبه‌ها (فقط برای شعبه‌های اصلی)

        // این شعبه به چه شعبه‌ای تعلق دارد (برای شعبه‌های فرعی)
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();  // کارمندان شعبه
        public ICollection<Student> Students { get; set; } = new List<Student>();  // هنرجویان شعبه
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();  // اساتید شعبه
        public ICollection<Secretary> Secretaries { get; set; } = new List<Secretary>();  // منشی‌ها
        public ICollection<Manager> Managers { get; set; } = new List<Manager>();  // مدیر شعبه
    }
}
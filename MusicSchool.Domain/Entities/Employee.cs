using MusicSchool.Domain.Common;

namespace MusicSchool.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;  // نام کامل کارمند
        public string Position { get; set; } = string.Empty;  // موقعیت شغلی

        public int BranchId { get; set; }  // ارتباط به شعبه
        public Branch? Branch { get; set; }  // شعبه مربوطه
    }
}
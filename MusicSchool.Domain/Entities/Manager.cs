namespace MusicSchool.Domain.Entities
{
    public class Manager : Employee
    {
        public string Department { get; set; } = string.Empty;  // دپارتمان یا بخش مربوطه
        public bool IsSuperAdmin { get; set; } = false;  // فلگ برای تبدیل به مدیر کل
    }
}
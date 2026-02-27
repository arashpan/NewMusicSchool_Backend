namespace MusicSchool.Domain.Entities
{
    public class SuperAdmin : Manager
    {
        // ویژگی‌های خاص مدیر کل (SuperAdmin)
        public string SpecialPermissions { get; set; } = "Full access";  // دسترسی‌های ویژه مدیر کل
    }
}
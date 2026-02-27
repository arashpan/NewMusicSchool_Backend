namespace MusicSchool.Application.DTOs
{
    public class UpdateProfileDTO
    {
        public string Username { get; set; }
        public string? FullName { get; set; }  // FullName به صورت nullable برای تغییر
        public DateTime UpdatedAt { get; set; }
    }
}
namespace MusicSchool.Application.DTOs.Students
{
    public class ParentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
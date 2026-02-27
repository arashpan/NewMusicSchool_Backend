using MusicSchool.Domain.Common;

namespace MusicSchool.Domain.Entities
{
    public class Parent : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
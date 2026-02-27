using System;

namespace MusicSchool.Application.DTOs.Students
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public int BranchId { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
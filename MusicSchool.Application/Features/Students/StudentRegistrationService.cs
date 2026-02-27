using System.Threading;
using System.Threading.Tasks;
using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Application.DTOs.Students;
using MusicSchool.Domain.Entities;

namespace MusicSchool.Application.Features.Students
{
    public class StudentRegistrationService : IStudentRegistrationService
    {
        private readonly IStudentRepository _repo;

        public StudentRegistrationService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<StudentDto> RegisterAsync(RegisterStudentRequestDto request, CancellationToken ct)
        {
            Parent? parent = null;

            if (request.Parent != null)
            {
                parent = new Parent
                {
                    FirstName = request.Parent.FirstName.Trim(),
                    LastName = request.Parent.LastName.Trim(),
                    PhoneNumber = request.Parent.PhoneNumber,
                    Email = request.Parent.Email
                };
            }

            var student = new Student
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BranchId = request.BranchId,
                Parent = parent
            };

            await _repo.AddAsync(student, ct);
            await _repo.SaveChangesAsync(ct);

            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                BranchId = student.BranchId,
                ParentId = student.ParentId,
                CreatedAt = student.CreatedAt,
                UpdatedAt = student.UpdatedAt
            };
        }
    }
}
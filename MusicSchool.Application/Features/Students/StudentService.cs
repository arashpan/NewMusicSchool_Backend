using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Application.DTOs.Students;
using MusicSchool.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MusicSchool.Application.Features.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        // ثبت نام دانش‌آموز
        public async Task<StudentDto> CreateAsync(RegisterStudentRequestDto request, CancellationToken ct)
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

        // گرفتن اطلاعات یک دانش‌آموز
        public async Task<StudentDto> GetByIdAsync(int id, CancellationToken ct)
        {
            var student = await _repo.GetByIdAsync(id, ct);
            if (student == null)
                return null;

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

        // گرفتن لیست تمام دانش‌آموزان
        public async Task<List<StudentDto>> GetAllAsync(CancellationToken ct)
        {
            var students = await _repo.GetAllAsync(ct);

            var studentDtos = new List<StudentDto>();
            foreach (var student in students)
            {
                studentDtos.Add(new StudentDto
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
                });
            }

            return studentDtos;
        }

        // به‌روزرسانی اطلاعات دانش‌آموز
        public async Task UpdateAsync(int id, UpdateStudentDto request, CancellationToken ct)
        {
            var student = await _repo.GetByIdAsync(id, ct);
            if (student != null)
            {
                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                student.DateOfBirth = request.DateOfBirth;
                student.PhoneNumber = request.PhoneNumber;
                student.Email = request.Email;
                student.BranchId = request.BranchId;
                student.ParentId = request.ParentId;

                await _repo.UpdateAsync(student, ct);
            }
        }

        // حذف دانش‌آموز
        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
        }
    }
}
using MusicSchool.Application.DTOs.Students;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Abstractions.Services
{
    public interface IStudentService
    {
        // ثبت نام دانش‌آموز
        Task<StudentDto> CreateAsync(RegisterStudentRequestDto request, CancellationToken ct);

        // گرفتن اطلاعات یک دانش‌آموز
        Task<StudentDto> GetByIdAsync(int id, CancellationToken ct);

        // گرفتن لیست تمام دانش‌آموزان
        Task<List<StudentDto>> GetAllAsync(CancellationToken ct);

        // به‌روزرسانی اطلاعات دانش‌آموز
        Task UpdateAsync(int id, UpdateStudentDto request, CancellationToken ct);

        // حذف دانش‌آموز
        Task DeleteAsync(int id, CancellationToken ct);
    }
}
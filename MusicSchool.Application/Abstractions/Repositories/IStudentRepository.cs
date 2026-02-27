using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Abstractions.Repositories
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student, CancellationToken ct);
        Task<Student> GetByIdAsync(int id, CancellationToken ct);
        Task<List<Student>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(Student student, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
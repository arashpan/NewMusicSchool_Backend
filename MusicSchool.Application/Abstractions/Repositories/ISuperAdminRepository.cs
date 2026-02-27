using MusicSchool.Domain.Entities;

namespace MusicSchool.Application.Abstractions.Repositories
{
    public interface ISuperAdminRepository
    {
        Task AddAsync(SuperAdmin superAdmin, CancellationToken ct);
        Task<SuperAdmin> GetByIdAsync(int id, CancellationToken ct);
        Task<List<SuperAdmin>> GetAllAsync(CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
using MusicSchool.Domain.Entities;

namespace MusicSchool.Application.Abstractions.Services
{
    public interface ISuperAdminService
    {
        Task<SuperAdmin> GetByIdAsync(int id, CancellationToken ct);
        Task<List<SuperAdmin>> GetAllAsync(CancellationToken ct);
        Task<SuperAdmin> CreateAsync(SuperAdmin superAdmin, CancellationToken ct);
        Task UpdateAsync(int id, SuperAdmin updatedSuperAdmin, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}

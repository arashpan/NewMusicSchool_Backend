using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Abstractions.Repositories
{
    public interface IManagerRepository
    {
        Task AddAsync(Manager manager, CancellationToken ct);
        Task<Manager> GetByIdAsync(int id, CancellationToken ct);
        Task<List<Manager>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(Manager manager, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
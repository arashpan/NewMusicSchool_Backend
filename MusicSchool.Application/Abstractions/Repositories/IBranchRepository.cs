using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Abstractions.Repositories
{
    public interface IBranchRepository
    {
        Task AddAsync(Branch branch, CancellationToken ct);
        Task<Branch> GetByIdAsync(int id, CancellationToken ct);
        Task<List<Branch>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(Branch branch, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
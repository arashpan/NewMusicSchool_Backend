using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Abstractions.Services
{
    public interface IBranchService
    {
        Task<Branch> CreateAsync(Branch branch, CancellationToken ct);
        Task<Branch> GetByIdAsync(int id, CancellationToken ct);
        Task<List<Branch>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(int id, Branch updatedBranch, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}
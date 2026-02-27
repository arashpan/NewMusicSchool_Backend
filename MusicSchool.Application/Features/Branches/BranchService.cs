using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Features.Branches
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repo;

        public BranchService(IBranchRepository repo)
        {
            _repo = repo;
        }

        public async Task<Branch> CreateAsync(Branch branch, CancellationToken ct)
        {
            // اگر parentBranchId ارسال شده باشد، باید ParentBranch از دیتابیس بارگذاری شود
            if (branch.ParentBranchId.HasValue)
            {
                var parentBranch = await _repo.GetByIdAsync(branch.ParentBranchId.Value, ct);
                if (parentBranch == null)
                {
                    throw new KeyNotFoundException("Invalid Parent Branch.");
                }
                branch.ParentBranch = parentBranch;  // پیوند دادن شعبه اصلی
            }

            await _repo.AddAsync(branch, ct);
            await _repo.SaveChangesAsync(ct);
            return branch;
        }

        public async Task<Branch> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _repo.GetByIdAsync(id, ct);
        }

        public async Task<List<Branch>> GetAllAsync(CancellationToken ct)
        {
            return await _repo.GetAllAsync(ct);
        }

        public async Task UpdateAsync(int id, Branch updatedBranch, CancellationToken ct)
        {
            var branch = await _repo.GetByIdAsync(id, ct);
            if (branch != null)
            {
                branch.Name = updatedBranch.Name;
                branch.Location = updatedBranch.Location;
                branch.ParentBranchId = updatedBranch.ParentBranchId;
                branch.ParentBranch = updatedBranch.ParentBranch;

                await _repo.UpdateAsync(branch, ct);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
        }
    }
}
using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Domain.Entities;
using MusicSchool.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _db;

        public BranchRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Branch branch, CancellationToken ct)
        {
            await _db.Branches.AddAsync(branch, ct);
        }

        public async Task<Branch> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _db.Branches
                             .Include(b => b.ParentBranch)      // بارگذاری شعبه اصلی
                             .Include(b => b.SubBranches)      // بارگذاری زیرشعبه‌ها
                             .FirstOrDefaultAsync(b => b.Id == id, ct);
        }

        public async Task<List<Branch>> GetAllAsync(CancellationToken ct)
        {
            return await _db.Branches.Include(b => b.ParentBranch).ToListAsync(ct);
        }

        public async Task UpdateAsync(Branch branch, CancellationToken ct)
        {
            _db.Branches.Update(branch);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var branch = await _db.Branches.FindAsync(id);
            if (branch != null)
            {
                _db.Branches.Remove(branch);
                await _db.SaveChangesAsync(ct);
            }
        }

        public Task SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
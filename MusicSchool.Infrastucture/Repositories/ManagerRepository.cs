using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Domain.Entities;
using MusicSchool.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Infrastructure.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly AppDbContext _db;

        public ManagerRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Manager manager, CancellationToken ct)
        {
            await _db.Managers.AddAsync(manager, ct);
        }

        public async Task<Manager> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _db.Managers
                             .Include(m => m.Branch) // بارگذاری شعبه
                             .FirstOrDefaultAsync(m => m.Id == id, ct);
        }

        public async Task<List<Manager>> GetAllAsync(CancellationToken ct)
        {
            return await _db.Managers.Include(m => m.Branch).ToListAsync(ct);
        }

        public async Task UpdateAsync(Manager manager, CancellationToken ct)
        {
            _db.Managers.Update(manager);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var manager = await _db.Managers.FindAsync(id);
            if (manager != null)
            {
                _db.Managers.Remove(manager);
                await _db.SaveChangesAsync(ct);
            }
        }

        public Task SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
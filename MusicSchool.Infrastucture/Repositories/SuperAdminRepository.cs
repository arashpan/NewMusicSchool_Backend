using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Domain.Entities;
using MusicSchool.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicSchool.Infrastructure.Repositories
{
    public class SuperAdminRepository : ISuperAdminRepository
    {
        private readonly AppDbContext _db;

        public SuperAdminRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(SuperAdmin superAdmin, CancellationToken ct)
        {
            await _db.SuperAdmins.AddAsync(superAdmin, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<SuperAdmin> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _db.SuperAdmins.FindAsync(id);
        }

        public async Task<List<SuperAdmin>> GetAllAsync(CancellationToken ct)
        {
            // مشخص کردن نوع داده که قرار است از دیتابیس گرفته شود
            return await _db.SuperAdmins.ToListAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var superAdmin = await _db.SuperAdmins.FindAsync(id);
            if (superAdmin != null)
            {
                _db.SuperAdmins.Remove(superAdmin);
                await _db.SaveChangesAsync(ct);
            }
        }

        public Task SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
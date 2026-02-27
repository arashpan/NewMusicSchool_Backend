using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Features.Managers
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _repo;

        public ManagerService(IManagerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Manager> CreateAsync(Manager manager, CancellationToken ct)
        {
            await _repo.AddAsync(manager, ct);
            await _repo.SaveChangesAsync(ct);
            return manager;
        }

        public async Task<Manager> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _repo.GetByIdAsync(id, ct);
        }

        public async Task<List<Manager>> GetAllAsync(CancellationToken ct)
        {
            return await _repo.GetAllAsync(ct);
        }

        public async Task UpdateAsync(int id, Manager updatedManager, CancellationToken ct)
        {
            var manager = await _repo.GetByIdAsync(id, ct);
            if (manager != null)
            {
                manager.FullName = updatedManager.FullName;
                manager.Position = updatedManager.Position;
                manager.BranchId = updatedManager.BranchId;
                await _repo.UpdateAsync(manager, ct);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
        }

        // تبدیل مدیر به مدیر کل
        public async Task<SuperAdmin> ConvertToSuperAdminAsync(int id, CancellationToken ct)
        {
            var manager = await _repo.GetByIdAsync(id, ct);
            if (manager != null && !manager.IsSuperAdmin)
            {
                manager.IsSuperAdmin = true;
                var superAdmin = new SuperAdmin
                {
                    FullName = manager.FullName,
                    Position = manager.Position,
                    BranchId = manager.BranchId,
                    SpecialPermissions = "Full access"
                };
                await _repo.UpdateAsync(manager, ct);
                return superAdmin;
            }
            return null;
        }
    }
}
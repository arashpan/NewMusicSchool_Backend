using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Features.SuperAdmins
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ISuperAdminRepository _superAdminRepository;

        public SuperAdminService(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;
        }

        public async Task<SuperAdmin> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _superAdminRepository.GetByIdAsync(id, ct);
        }

        public async Task<List<SuperAdmin>> GetAllAsync(CancellationToken ct)
        {
            return await _superAdminRepository.GetAllAsync(ct);
        }

        public async Task<SuperAdmin> CreateAsync(SuperAdmin superAdmin, CancellationToken ct)
        {
            await _superAdminRepository.AddAsync(superAdmin, ct);
            return superAdmin;
        }

        public async Task UpdateAsync(int id, SuperAdmin updatedSuperAdmin, CancellationToken ct)
        {
            var superAdmin = await _superAdminRepository.GetByIdAsync(id, ct);
            if (superAdmin != null)
            {
                superAdmin.FullName = updatedSuperAdmin.FullName;
                superAdmin.SpecialPermissions = updatedSuperAdmin.SpecialPermissions;
                await _superAdminRepository.SaveChangesAsync(ct);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var superAdmin = await _superAdminRepository.GetByIdAsync(id, ct);
            if (superAdmin != null)
            {
                await _superAdminRepository.DeleteAsync(id, ct);
            }
        }
    }
}

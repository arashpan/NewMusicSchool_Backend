using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Abstractions.Services
{
    public interface IManagerService
    {
        Task<Manager> CreateAsync(Manager manager, CancellationToken ct);
        Task<Manager> GetByIdAsync(int id, CancellationToken ct);
        Task<List<Manager>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(int id, Manager updatedManager, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);

        // تبدیل مدیر به مدیر کل
        Task<SuperAdmin> ConvertToSuperAdminAsync(int id, CancellationToken ct);
    }
}
using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Abstractions.Repositories
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee, CancellationToken ct);
        Task<Employee> GetByIdAsync(int id, CancellationToken ct);
        Task<List<Employee>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(Employee employee, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
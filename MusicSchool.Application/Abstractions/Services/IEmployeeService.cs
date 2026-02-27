using MusicSchool.Domain.Entities;

namespace MusicSchool.Application.Abstractions.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(Employee employee, CancellationToken ct);
        Task<Employee> GetByIdAsync(int id, CancellationToken ct);
        Task<List<Employee>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(int id, Employee updatedEmployee, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}
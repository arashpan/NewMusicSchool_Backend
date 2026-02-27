using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Application.Features.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Employee> CreateAsync(Employee employee, CancellationToken ct)
        {
            await _repo.AddAsync(employee, ct);
            await _repo.SaveChangesAsync(ct);
            return employee;
        }

        public async Task<Employee> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _repo.GetByIdAsync(id, ct);
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken ct)
        {
            return await _repo.GetAllAsync(ct);
        }

        public async Task UpdateAsync(int id, Employee updatedEmployee, CancellationToken ct)
        {
            var employee = await _repo.GetByIdAsync(id, ct);
            if (employee != null)
            {
                employee.FullName = updatedEmployee.FullName;
                employee.Position = updatedEmployee.Position;
                employee.BranchId = updatedEmployee.BranchId;

                await _repo.UpdateAsync(employee, ct);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
        }
    }
}
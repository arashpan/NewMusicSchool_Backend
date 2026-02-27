using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Domain.Entities;
using MusicSchool.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;

        public EmployeeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Employee employee, CancellationToken ct)
        {
            await _db.Employees.AddAsync(employee, ct);
        }

        public async Task<Employee> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _db.Employees
                             .Include(e => e.Branch)
                             .FirstOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken ct)
        {
            return await _db.Employees.Include(e => e.Branch).ToListAsync(ct);
        }

        public async Task UpdateAsync(Employee employee, CancellationToken ct)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee != null)
            {
                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync(ct);
            }
        }

        public Task SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
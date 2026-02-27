using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Domain.Entities;
using MusicSchool.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _db;

        public StudentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Student student, CancellationToken ct)
        {
            await _db.Students.AddAsync(student, ct);
        }

        public async Task<Student> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _db.Students.Include(s => s.Parent)
                                      .FirstOrDefaultAsync(s => s.Id == id, ct);
        }

        public async Task<List<Student>> GetAllAsync(CancellationToken ct)
        {
            return await _db.Students.Include(s => s.Parent).ToListAsync(ct);
        }

        public async Task UpdateAsync(Student student, CancellationToken ct)
        {
            _db.Students.Update(student);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var student = await _db.Students.FindAsync(id);
            if (student != null)
            {
                _db.Students.Remove(student);
                await _db.SaveChangesAsync(ct);
            }
        }

        public Task SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
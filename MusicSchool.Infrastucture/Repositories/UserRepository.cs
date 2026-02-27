using MusicSchool.Infrastructure.Persistence;
using MusicSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MusicSchool.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                                 .SingleOrDefault(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
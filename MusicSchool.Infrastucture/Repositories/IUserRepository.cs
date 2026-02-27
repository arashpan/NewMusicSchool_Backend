using MusicSchool.Domain.Entities;

namespace MusicSchool.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        void AddUser(User user);
        void SaveChanges();
    }
}

using VelarisBackend.Models;

namespace VelarisBackend.Repositories
{
    public interface IUserRepository
    {
        User GetByUserName(string username);
        void Add(User user);
        void SaveChanges();
    }
}
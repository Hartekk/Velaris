
using VelarisBackend.Models;

namespace VelarisBackend.Repositories
{
    public interface IAuthTokenRepository
    {
        void Add(AuthToken token);
        AuthToken Get(string token);
        void Remove(AuthToken token);
        void SaveChanges();
    }
}
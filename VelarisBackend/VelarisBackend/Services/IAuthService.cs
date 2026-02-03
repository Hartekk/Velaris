

namespace VelarisBackend.Services
{
    public interface IAuthService
    {
        void Register(string username, string password, string email);
        string Login(string username, string password);
    }
}

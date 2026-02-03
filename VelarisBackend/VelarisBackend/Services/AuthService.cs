

using System;
using VelarisBackend.Infrastructure;
using VelarisBackend.Repositories;

namespace VelarisBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(string username, string password, string email)
        {
            var existingUser = _userRepository.GetByUserName(username); 
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new Models.User
            {
                Username = username,
                PasswordHash = passwordHash,
                Email = email,
            };

            _userRepository.Add(user);
        }

        public string Login(string username, string password)
        {
            var user = _userRepository.GetByUserName(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password.");
            }
            
            return JwtTokenGenerator.GenerateToken(user);
        }
    }
}
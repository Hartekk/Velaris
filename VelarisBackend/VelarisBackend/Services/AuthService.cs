using System;
using VelarisBackend.Infrastructure;
using VelarisBackend.Repositories;

namespace VelarisBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthTokenRepository _authTokenRepository;

        public AuthService(IUserRepository userRepository, IAuthTokenRepository authTokenRepository)
        {
            _userRepository = userRepository;
            _authTokenRepository = authTokenRepository;
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
            _userRepository.SaveChanges();
        }

        public string Login(string username, string password)
        {
            var user = _userRepository.GetByUserName(username);

            if (user == null)
            {
                throw new Exception("USER NOT FOUND");
            }
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new Exception("PASSWORD INVALID");
            }
            
            var token = JwtTokenGenerator.GenerateToken(user);

            _authTokenRepository.Add(new Models.AuthToken
            {
                UserId = user.Id,
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            });

            _authTokenRepository.SaveChanges();

            return token;
        }
    }
}
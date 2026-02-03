using System;
using System.Linq;
using VelarisBackend.Infrastructure;
using VelarisBackend.Models;

namespace VelarisBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public User GetByUserName(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void SaveChanges()
        {
           _context.SaveChanges();
        }
    }
}
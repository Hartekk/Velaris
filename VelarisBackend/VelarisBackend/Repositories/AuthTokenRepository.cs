using System;
using System.Linq;
using VelarisBackend.Infrastructure;
using VelarisBackend.Models;

namespace VelarisBackend.Repositories
{
    public class AuthTokenRepository : IAuthTokenRepository
    {

        public readonly DatabaseContext _context;

        public AuthTokenRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Add(AuthToken token)
        {
            _context.AuthTokens.Add(token);
        }

        public AuthToken Get(string token)
        {
            return _context.AuthTokens.FirstOrDefault(t => t.Token == token);
        }

        public void Remove(AuthToken token)
        {
            _context.AuthTokens.Remove(token);
        }

        public void SaveChanges()
        {
           _context.SaveChanges();
        }
    }
}
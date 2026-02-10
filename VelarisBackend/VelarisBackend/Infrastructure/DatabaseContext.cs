using System.Data.Entity;
using VelarisBackend.Models;

namespace VelarisBackend.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("VelarisDb") { }
        
            public DbSet<User> Users { get; set; }
            public DbSet<TodoItem> TodoItems { get; set; }
            public DbSet<AuthToken> AuthTokens { get; set; }
    }
}
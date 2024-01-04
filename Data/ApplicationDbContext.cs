using Microsoft.EntityFrameworkCore;
using TODOApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TODOApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPassword> userPasswords { get; set; }
    }
}

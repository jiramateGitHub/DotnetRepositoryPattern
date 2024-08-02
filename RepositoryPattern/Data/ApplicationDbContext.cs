using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RepositoryPattern.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}

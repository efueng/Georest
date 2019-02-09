using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Georest.Domain.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<UserLoginToken> UserLoginTokens { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<>()
        }
    }
}

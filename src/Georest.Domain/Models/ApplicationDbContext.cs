using Microsoft.EntityFrameworkCore;

namespace Georest.Domain.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Group>().HasIndex(g => g.Name).IsUnique();
            modelBuilder.Entity<Lab>().HasIndex(l => l.Title).IsUnique();

            //modelBuilder.Entity<GroupUser>().HasKey(gu => new { gu.UserId, gu.GroupId });

            //modelBuilder.Entity<GroupUser>()
            //    .HasOne(gu => gu.User)
            //    .WithMany(u => u.GroupUsers)
            //    .HasForeignKey(gu => gu.UserId);

            //modelBuilder.Entity<GroupUser>()
            //    .HasOne(gu => gu.Group)
            //    .WithMany(g => g.GroupUsers)
            //    .HasForeignKey(gu => gu.GroupId);

            
        }
    }
}
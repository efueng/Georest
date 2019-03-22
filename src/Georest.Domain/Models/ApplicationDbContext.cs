using Microsoft.EntityFrameworkCore;

namespace Georest.Domain.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorLab> InstructorLabs { get; set; }
        public DbSet<StudentLab> StudentLabs { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<InstructorResponse> InstructorResponses { get; set; }
        public DbSet<StudentResponse> StudentResponses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lab>().HasIndex(l => l.Title).IsUnique();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using _1617_2_LI41N_G9.Models;

namespace _1617_2_LI41N_G9.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Course)
                .WithMany(c => c.Classes)
                .HasForeignKey(c => c.CourseId)
                .HasConstraintName("ForeignKey_Course_Class");

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Class)
                .WithMany(c => c.Groups)
                .HasForeignKey(g => new { g.ClassId, g.Semester })
                .HasConstraintName("ForeignKey_Group_Class");

            modelBuilder.Entity<Group>()
                .HasKey(g => new { g.ClassId, g.Semester, g.GroupNumber });

            modelBuilder.Entity<Class>()
                .HasKey(c => new { c.CourseId, c.Semester });
        }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

    }
}
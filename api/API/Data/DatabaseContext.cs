using System.Diagnostics.Contracts;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DatabaseContext() {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Group> Groups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // Courses
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Acronym)
                .IsUnique();

            // Relation Class N:N Teacher
            modelBuilder.Entity<ClassTeacher>()
                .HasKey(c => new { c.ClassId, c.TeacherId });

            modelBuilder.Entity<ClassTeacher>()
                .HasOne(c => c.Class)
                .WithMany(c => c.Teachers)
                .HasForeignKey(c => c.ClassId);

            modelBuilder.Entity<ClassTeacher>()
                .HasOne(c => c.Teacher)
                .WithMany(c => c.Classes)
                .HasForeignKey(c => c.TeacherId);

            // Relation Class N:N Student
            modelBuilder.Entity<ClassStudent>()
                .HasKey(c => new { c.ClassId, c.StudentId });

            modelBuilder.Entity<ClassStudent>()
                .HasOne(c => c.Class)
                .WithMany(c => c.Participants)
                .HasForeignKey(c => c.ClassId);

            modelBuilder.Entity<ClassStudent>()
                .HasOne(c => c.Student)
                .WithMany(c => c.Classes)
                .HasForeignKey(c => c.StudentId);

            // Relation Group N:N Student
            modelBuilder.Entity<GroupStudent>()
                .HasKey(c => new { c.StudentNumber, c.GroupId });

            modelBuilder.Entity<GroupStudent>()
                .HasOne(c => c.Student)
                .WithMany(c => c.Groups)
                .HasForeignKey(c => c.StudentNumber);

            modelBuilder.Entity<GroupStudent>()
                .HasOne(c => c.Group)
                .WithMany(c => c.Students)
                .HasForeignKey(c => new { c.GroupId });

        }
    }
}
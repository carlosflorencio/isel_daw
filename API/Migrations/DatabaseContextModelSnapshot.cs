using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using API.Models;

namespace API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("DAW_API.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<int>("MaxGroupSize");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("SemesterId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("SemesterId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("DAW_API.Models.ClassStudent", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("StudentId");

                    b.HasKey("ClassId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("ClassStudent");
                });

            modelBuilder.Entity("DAW_API.Models.ClassTeacher", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("TeacherId");

                    b.HasKey("ClassId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("ClassTeacher");
                });

            modelBuilder.Entity("DAW_API.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Acronym")
                        .IsRequired();

                    b.Property<int>("CoordinatorId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Acronym")
                        .IsUnique();

                    b.HasIndex("CoordinatorId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("DAW_API.Models.Group", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("Id");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.HasKey("ClassId", "Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DAW_API.Models.GroupStudent", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("StudentId");

                    b.Property<int>("GroupId");

                    b.HasKey("ClassId", "StudentId", "GroupId");

                    b.HasIndex("GroupId", "ClassId");

                    b.ToTable("GroupStudent");
                });

            modelBuilder.Entity("DAW_API.Models.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Term");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("DAW_API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("DAW_API.Models.Administrator", b =>
                {
                    b.HasBaseType("DAW_API.Models.User");


                    b.ToTable("Administrator");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("DAW_API.Models.Student", b =>
                {
                    b.HasBaseType("DAW_API.Models.User");

                    b.Property<int>("Number");

                    b.ToTable("Student");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("DAW_API.Models.Teacher", b =>
                {
                    b.HasBaseType("DAW_API.Models.User");

                    b.Property<int>("Number");

                    b.ToTable("Teacher");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("DAW_API.Models.Class", b =>
                {
                    b.HasOne("DAW_API.Models.Course", "Course")
                        .WithMany("Classes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAW_API.Models.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAW_API.Models.ClassStudent", b =>
                {
                    b.HasOne("DAW_API.Models.Class", "Class")
                        .WithMany("Participants")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAW_API.Models.Student", "Student")
                        .WithMany("Classes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAW_API.Models.ClassTeacher", b =>
                {
                    b.HasOne("DAW_API.Models.Class", "Class")
                        .WithMany("Teachers")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAW_API.Models.Teacher", "Teacher")
                        .WithMany("Classes")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAW_API.Models.Course", b =>
                {
                    b.HasOne("DAW_API.Models.Teacher", "Coordinator")
                        .WithMany()
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAW_API.Models.Group", b =>
                {
                    b.HasOne("DAW_API.Models.Class", "Class")
                        .WithMany("Groups")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAW_API.Models.GroupStudent", b =>
                {
                    b.HasOne("DAW_API.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAW_API.Models.Student", "Student")
                        .WithMany("Groups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAW_API.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId", "ClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

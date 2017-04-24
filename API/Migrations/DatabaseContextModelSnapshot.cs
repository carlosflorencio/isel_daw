using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using API.Data;
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

            modelBuilder.Entity("API.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AutoEnrollment");

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

            modelBuilder.Entity("API.Models.ClassStudent", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("StudentNumberId");

                    b.HasKey("ClassId", "StudentNumberId");

                    b.HasIndex("StudentNumberId");

                    b.ToTable("ClassStudent");
                });

            modelBuilder.Entity("API.Models.ClassTeacher", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("TeacherId");

                    b.HasKey("ClassId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("ClassTeacher");
                });

            modelBuilder.Entity("API.Models.Course", b =>
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

            modelBuilder.Entity("API.Models.Group", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.HasKey("ClassId", "Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("API.Models.GroupStudent", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("StudentNumber");

                    b.Property<int>("GroupId");

                    b.HasKey("ClassId", "StudentNumber", "GroupId");

                    b.HasIndex("StudentNumber");

                    b.HasIndex("ClassId", "GroupId");

                    b.ToTable("GroupStudent");
                });

            modelBuilder.Entity("API.Models.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Term");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("API.Models.Student", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("Number");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("API.Models.Teacher", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("Number");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("API.Models.Class", b =>
                {
                    b.HasOne("API.Models.Course", "Course")
                        .WithMany("Classes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.ClassStudent", b =>
                {
                    b.HasOne("API.Models.Class", "Class")
                        .WithMany("Participants")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Student", "Student")
                        .WithMany("Classes")
                        .HasForeignKey("StudentNumberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.ClassTeacher", b =>
                {
                    b.HasOne("API.Models.Class", "Class")
                        .WithMany("Teachers")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Teacher", "Teacher")
                        .WithMany("Classes")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Course", b =>
                {
                    b.HasOne("API.Models.Teacher", "Coordinator")
                        .WithMany()
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Group", b =>
                {
                    b.HasOne("API.Models.Class", "Class")
                        .WithMany("Groups")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.GroupStudent", b =>
                {
                    b.HasOne("API.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Student", "Student")
                        .WithMany("Groups")
                        .HasForeignKey("StudentNumber")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("ClassId", "GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

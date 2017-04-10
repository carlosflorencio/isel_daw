using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using _1617_2_LI41N_G9.Data;

namespace _16172LI41NG9.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170410165055_coordinatorId")]
    partial class coordinatorId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Class", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<string>("Semester");

                    b.HasKey("CourseId", "Semester");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Acronym")
                        .IsRequired();

                    b.Property<int>("CoordinatorId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Group", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<string>("Semester");

                    b.Property<int>("GroupNumber");

                    b.HasKey("ClassId", "Semester", "GroupNumber");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("ClassCourseId");

                    b.Property<string>("ClassSemester");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int?>("GroupClassId");

                    b.Property<int?>("GroupNumber");

                    b.Property<string>("GroupSemester");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ClassCourseId", "ClassSemester");

                    b.HasIndex("GroupClassId", "GroupSemester", "GroupNumber");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("ClassCourseId");

                    b.Property<string>("ClassSemester");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ClassCourseId", "ClassSemester");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Class", b =>
                {
                    b.HasOne("_1617_2_LI41N_G9.Models.Course", "Course")
                        .WithMany("Classes")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("ForeignKey_Course_Class")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Course", b =>
                {
                    b.HasOne("_1617_2_LI41N_G9.Models.Teacher", "Coordinator")
                        .WithMany()
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Group", b =>
                {
                    b.HasOne("_1617_2_LI41N_G9.Models.Class", "Class")
                        .WithMany("Groups")
                        .HasForeignKey("ClassId", "Semester")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Student", b =>
                {
                    b.HasOne("_1617_2_LI41N_G9.Models.Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassCourseId", "ClassSemester");

                    b.HasOne("_1617_2_LI41N_G9.Models.Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupClassId", "GroupSemester", "GroupNumber");
                });

            modelBuilder.Entity("_1617_2_LI41N_G9.Models.Teacher", b =>
                {
                    b.HasOne("_1617_2_LI41N_G9.Models.Class")
                        .WithMany("Teachers")
                        .HasForeignKey("ClassCourseId", "ClassSemester");
                });
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _16172LI41NG9.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false),
                    Semester = table.Column<string>(nullable: false),
                    GroupNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => new { x.ClassId, x.Semester, x.GroupNumber });
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClassCourseId = table.Column<int>(nullable: true),
                    ClassSemester = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    GroupClassId = table.Column<int>(nullable: true),
                    GroupNumber = table.Column<int>(nullable: true),
                    GroupSemester = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupClassId_GroupSemester_GroupNumber",
                        columns: x => new { x.GroupClassId, x.GroupSemester, x.GroupNumber },
                        principalTable: "Groups",
                        principalColumns: new[] { "ClassId", "Semester", "GroupNumber" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClassCourseId = table.Column<int>(nullable: true),
                    ClassSemester = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Acronym = table.Column<string>(nullable: false),
                    CoordinatorId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_CoordinatorId",
                        column: x => x.CoordinatorId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false),
                    Semester = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => new { x.CourseId, x.Semester });
                    table.ForeignKey(
                        name: "ForeignKey_Course_Class",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CoordinatorId",
                table: "Courses",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassCourseId_ClassSemester",
                table: "Students",
                columns: new[] { "ClassCourseId", "ClassSemester" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupClassId_GroupSemester_GroupNumber",
                table: "Students",
                columns: new[] { "GroupClassId", "GroupSemester", "GroupNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ClassCourseId_ClassSemester",
                table: "Teachers",
                columns: new[] { "ClassCourseId", "ClassSemester" });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Classes_ClassId_Semester",
                table: "Groups",
                columns: new[] { "ClassId", "Semester" },
                principalTable: "Classes",
                principalColumns: new[] { "CourseId", "Semester" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassCourseId_ClassSemester",
                table: "Students",
                columns: new[] { "ClassCourseId", "ClassSemester" },
                principalTable: "Classes",
                principalColumns: new[] { "CourseId", "Semester" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Classes_ClassCourseId_ClassSemester",
                table: "Teachers",
                columns: new[] { "ClassCourseId", "ClassSemester" },
                principalTable: "Classes",
                principalColumns: new[] { "CourseId", "Semester" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Course_Class",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class SeedDatabaseData
    {

        public static void EnsureSeedDataForContext(this DatabaseContext context) {

            //            context.Database.EnsureDeleted();
            //            context.Database.EnsureCreated();
            var teachers = AddTeachers(context);

            var students = AddStudents(context);

            var courses = AddCourses(context, teachers);

            var semesters = AddSemesters(context);

            var classes = AddClasses(context, courses, semesters);

            context.SaveChanges(); // fill the entities with the ID from the DB

            classes[0].Participants = new List<ClassStudent>() {
                new ClassStudent() {
                    Student = students[0],
                    Class = classes[0]
                }
            };

            classes[0].Groups = new List<Group>() {
                new Group() {
                    Class = classes[0],
                }
            };

            // Persist all data
            context.SaveChanges();

            classes[0].Groups[0].Students = new List<GroupStudent>() {
                new GroupStudent() {
                    Student = students[0],
                    Class = classes[0]
                }
            };

            context.SaveChanges();
        }

        public static void ClearAllData(this DatabaseContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM \"ClassStudent\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"ClassTeacher\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"GroupStudent\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Classes\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Groups\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Students\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Teachers\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Courses\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Semesters\"");
        }

        private static Class[] AddClasses(DatabaseContext context, Course[] courses, Semester[] semesters) {
            var classes = new[] {
                new Class() {
                    Name = "D1",
                    Course = courses[0],
                    MaxGroupSize = 3,
                    Semester = semesters[0]
                },
                new Class() {
                    Name = "N1",
                    Course = courses[0],
                    MaxGroupSize = 3,
                    Semester = semesters[0]
                }
            };

            context.Classes.AddRange(classes);

            return classes;
        }

        private static Semester[] AddSemesters(DatabaseContext context) {
            var semesters = new[] {
                new Semester() {
                    Term = Term.Winter,
                    Year = 2016
                },
                new Semester() {
                    Term = Term.Spring,
                    Year = 2017
                }
            };
            context.Semesters.AddRange(semesters);
            return semesters;
        }

        private static Course[] AddCourses(DatabaseContext context, Teacher[] teachers) {
            var courses = new[] {
                new Course() {
                    Acronym = "LS",
                    Name = "Laboratorio de Software",
                    Coordinator = teachers[0],
                }
            };
            context.Courses.AddRange(courses);
            return courses;
        }

        private static Student[] AddStudents(DatabaseContext context) {
            var students = new[] {
                new Student {
                    Email = "carlos@gmail.com",
                    Name = "Carlos Florencio",
                    Number = 39250,
                    Password = "123456"
                },
                new Student {
                    Email = "nuno@gmail.com",
                    Name = "Nuno Reis",
                    Number = 35248,
                    Password = "123456"
                },
                new Student {
                    Email = "teste@gmail.com",
                    Name = "Teste Maricas",
                    Number = 35564,
                    Password = "123456"
                }
            };
            context.Students.AddRange(students);

            var randomStudents = new LinkedList<Student>();
            for (int i = 0; i < 50; i++) {
                var num = 200 + i;

                randomStudents.AddLast(new Student() {
                    Number = num,
                    Email = num + "@gmail.com",
                    Name = num + " Name",
                    Password = "123456"
                });
            }

            context.Students.AddRange(randomStudents);

            return students;
        }

        private static Teacher[] AddTeachers(DatabaseContext context) {
            var teachers = new[] {
                new Teacher() {
                    Name = "Pedro Félix",
                    Email = "pfelix@gmail.com",
                    Number = 1456,
                    Password = "123456",
                    IsAdmin = true
                },
                new Teacher() {
                    Name = "Freitas",
                    Email = "freitas@gmail.com",
                    Number = 3512,
                    Password = "123456"
                }
            };

            context.Teachers.AddRange(teachers);
            return teachers;
        }
    }
}

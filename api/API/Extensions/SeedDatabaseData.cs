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
        public static void ClearAllData(this DatabaseContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM \"ClassStudent\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"ClassTeacher\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"GroupStudent\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Classes\"");
            context.Database.ExecuteSqlCommand("ALTER SEQUENCE \"Classes_Id_seq\" RESTART WITH 1");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Groups\"");
            context.Database.ExecuteSqlCommand("ALTER SEQUENCE \"Groups_Id_seq\" RESTART WITH 1");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Students\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Teachers\"");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Courses\"");
            context.Database.ExecuteSqlCommand("ALTER SEQUENCE \"Courses_Id_seq\" RESTART WITH 1");
            context.Database.ExecuteSqlCommand("DELETE FROM \"Semesters\"");
            context.Database.ExecuteSqlCommand("ALTER SEQUENCE \"Semesters_Id_seq\" RESTART WITH 1");
        }

        public static void EnsureSeedDataForContext(this DatabaseContext context) {
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
                    Name = "Fernando Sousa",
                    Email = "sousa@gmail.com",
                    Number = 7890,
                    Password = "123456"
                },
                new Teacher() {
                    Name = "Miguel Gamboa Carvalho",
                    Email = "gamboa@gmail.com",
                    Number = 3512,
                    Password = "123456"
                },
                new Teacher() {
                    Name = "Vitor Almeida",
                    Email = "almeida@gmail.com",
                    Number = 3909,
                    Password = "123456"
                },
                new Teacher() {
                    Name = "José Simão",
                    Email = "simao@gmail.com",
                    Number = 1233,
                    Password = "123456"
                }
            };

            context.Teachers.AddRange(teachers);
            return teachers;
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
                    Name = "Teste",
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

        private static Course[] AddCourses(DatabaseContext context, Teacher[] teachers) {
            var courses = new[] {
                new Course() {
                    Acronym = "LS",
                    Name = "Laboratorio de Software",
                    Coordinator = teachers[0],
                },
                new Course() {
                    Acronym = "DAW",
                    Name = "Desenvolvimento de Aplicações Web",
                    Coordinator = teachers[0],
                },
                new Course() {
                    Acronym = "PS",
                    Name = "Projeto e Seminário",
                    Coordinator = teachers[1],
                },
                new Course() {
                    Acronym = "PC",
                    Name = "Programação Concorrente",
                    Coordinator = teachers[0],
                },
                new Course() {
                    Acronym = "PI",
                    Name = "Programação na Internet",
                    Coordinator = teachers[2],
                },
                new Course() {
                    Acronym = "RI",
                    Name = "Redes de Internet",
                    Coordinator = teachers[3],
                }
            };
            context.Courses.AddRange(courses);
            return courses;
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

        private static Class[] AddClasses(DatabaseContext context, Course[] courses, Semester[] semesters) {
            var classes = new[] {
                new Class() {
                    Name = "1N",
                    Course = courses[1],    //DAW
                    MaxGroupSize = 3,
                    Semester = semesters[1]
                },
                new Class() {
                    Name = "1D",
                    Course = courses[0],    //LS
                    MaxGroupSize = 3,
                    Semester = semesters[0]
                },
                new Class() {
                    Name = "1D",
                    Course = courses[2],    //PS
                    MaxGroupSize = 3,
                    Semester = semesters[1]
                },
                new Class() {
                    Name = "1N",
                    Course = courses[2],    //PS
                    MaxGroupSize = 3,
                    Semester = semesters[1]
                },
                new Class() {
                    Name = "1D",
                    Course = courses[3],    //PC
                    MaxGroupSize = 3,
                    Semester = semesters[0]
                },
                new Class() {
                    Name = "1N",
                    Course = courses[3],    //PC
                    MaxGroupSize = 3,
                    Semester = semesters[0]
                },
                new Class() {
                    Name = "1D",
                    Course = courses[4],    //PI
                    MaxGroupSize = 3,
                    Semester = semesters[0]
                },
                new Class() {
                    Name = "1N",
                    Course = courses[4],    //PI
                    MaxGroupSize = 3,
                    Semester = semesters[0]
                }
            };

            context.Classes.AddRange(classes);

            return classes;
        }

        private static Group[] AddGroups(DatabaseContext context, Class[] classes, Student[] students){
            return null;
        }
    }
}

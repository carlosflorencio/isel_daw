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

        public static async Task EnsureSeedDataForContext(this DatabaseContext context) {

            var teachers = new[] {
                new Teacher() {
                    Name = "Pedro Félix",
                    Email = "pfelix@gmail.com",
                    Number = 1456
                },
                new Teacher() {
                    Name = "Freitas",
                    Email = "freitas@gmail.com",
                    Number = 3512
                }
            };
            context.Teachers.AddRange(teachers);

            var students = new[] {
                new Student
                {
                    Email = "carlos@gmail.com",
                    Name = "Carlos Florencio",
                    Number = 39250
                },
                new Student
                {
                    Email = "nuno@gmail.com",
                    Name = "Nuno Reis",
                    Number = 35248
                },
                new Student
                {
                    Email = "teste@gmail.com",
                    Name = "Teste Maricas",
                    Number = 35564
                },
            };
            context.Students.AddRange(students);

            var courses = new[] {
                new Course() {
                    Acronym = "LS",
                    Name = "Laboratorio de Software",
                    Coordinator = teachers[0],
                }
            };
            context.Courses.AddRange(courses);

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

            await context.SaveChangesAsync();
        }
    }
}

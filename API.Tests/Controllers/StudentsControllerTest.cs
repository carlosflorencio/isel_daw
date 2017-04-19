using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace API.Tests.Controllers
{
    // http://docs.mytestedasp.net/tutorial/organizingtests.html
    public class StudentsControllerTest : MyController<StudentsController>
    {
        [Fact]
        public void Test_GetAll()
        {
            Instance()
            .WithDbContext(dbContext => dbContext.WithSet<Student>(e => e.AddRange(GetStudents())))
            .Calling(c => c.Get())
            .ShouldReturn()
            .Ok()
            .WithModelOfType<List<Student>>()
            .Passing(model => model.Count == 3);

        }


        private static Student[] GetStudents()
        {
            return new[] {
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
        }
    }
}

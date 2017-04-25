using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Contracts;
using API.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace API.Tests.Data
{
//    public class StudentRepositoryTest : IDisposable
//    {
//
//        private readonly SqliteConnection _connection;
//        private readonly DatabaseContext _context;
//        private readonly IStudentRepository _repo;
//
//        // This class is instatiated for each test
//        // https://xunit.github.io/docs/shared-context.html
//        public StudentRepositoryTest()
//        {
//            // In-memory database only exists while the connection is open
//            _connection = new SqliteConnection("DataSource=:memory:");
//            _connection.Open();
//
//            var options = new DbContextOptionsBuilder<DatabaseContext>()
//                .UseSqlite(_connection)
//                .Options;
//
//            _context = new DatabaseContext(options);
//            _context.Database.EnsureCreated();
//
//            _repo = new StudentRepository(_context);
//        }
//
//        public void Dispose()
//        {
//            _connection.Close();
//            _context.Dispose();
//        }
//
//
//        [Fact]
//        public async Task Test_Insert_Student()
//        {
//            await _repo.AddAsync(GetStudents()[0]);
//
//            var students = _context.Students.ToList();
//
//            Assert.Equal(1, students.Count);
//        }
//
//        [Fact]
//        public async Task Test_Delete_Student()
//        {
//            var stds = GetStudents();
//            _context.Students.AddRange(stds);
//            _context.SaveChanges();
//
//            var toRemove = stds[0];
//            var ok = await _repo.DeleteAsync(toRemove);
//
//            Assert.True(ok);
//
//            Assert.Equal(stds.Length - 1, (await _repo.GetAllAsync()).Count());
//
//            var first = await _repo.GetByNumberAsync(stds[0].Number);
//            Assert.Null(first);
//        }
//
//        [Fact]
//        public async Task Test_Edit_Student()
//        {
//            var studentOriginal = GetStudents()[0];
//            _context.Students.Add(studentOriginal);
//            _context.SaveChanges();
//
//            var student = await _repo.GetByNumberAsync(studentOriginal.Number);
//            student.Name = "My new Name!";
//            var ok = await _repo.EditAsync(student);
//
//            Assert.True(ok);
//
//            var studentEdited = await _repo.GetByNumberAsync(studentOriginal.Number);
//
//            Assert.Equal(studentOriginal.Number, studentEdited.Number);
//            Assert.Equal(studentOriginal.Email, studentEdited.Email);
//            Assert.Equal("My new Name!", studentEdited.Name);
//        }
//
//        [Fact]
//        public async Task Test_FindBy_Student()
//        {
//            _context.Students.AddRange(GetStudents());
//            _context.SaveChanges();
//
//            var student = (await _repo.FindByAsync(s => s.Email == "teste@gmail.com"))
//                .FirstOrDefault();
//            Assert.Equal(35564, student.Number);
//        }
//
//        /*
//        |--------------------------------------------------------------------------
//        | Utils
//        |--------------------------------------------------------------------------
//        */
//        private static Student[] GetStudents()
//        {
//            return new[] {
//                new Student
//                {
//                    Email = "carlos@gmail.com",
//                    Name = "Carlos Florencio",
//                    Number = 39250
//                },
//                new Student
//                {
//                    Email = "nuno@gmail.com",
//                    Name = "Nuno Reis",
//                    Number = 35248
//                },
//                new Student
//                {
//                    Email = "teste@gmail.com",
//                    Name = "Teste Maricas",
//                    Number = 35564
//                },
//            };
//        }
//
//    }
}

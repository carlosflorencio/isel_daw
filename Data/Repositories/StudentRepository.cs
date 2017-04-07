using System.Collections.Generic;
using System.Linq;
using _1617_2_LI41N_G9.Models;

namespace _1617_2_LI41N_G9.Data.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly DatabaseContext _context;

        public StudentRepository(DatabaseContext context)
        {
            _context = context;

            if (!_context.Students.Any()) {
                Add(new Student { Name = "Student1", Email = "gmail.com" });
                Add(new Student { Name = "Student2", Email = "gmail.com" });
                Add(new Student { Name = "Student3", Email = "gmail.com" });
            }
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public void Add(Student item)
        {
            _context.Students.Add(item);
            _context.SaveChanges();
        }

        public Student Find(long key)
        {
            return _context.Students.FirstOrDefault(t => t.Key == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Students.First(t => t.Key == key);
            _context.Students.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Student item)
        {
            _context.Students.Update(item);
            _context.SaveChanges();
        }
    }
}

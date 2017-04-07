using System;
using System.Collections.Generic;
using System.Linq;
using _1617_2_LI41N_G9.Models;

namespace _1617_2_LI41N_G9.Data.Repositories
{
    public class TeacherRepository : IRepository<Teacher>
    {
        private readonly DatabaseContext _context;

        public TeacherRepository(DatabaseContext context)
        {
            _context = context;

            if (!_context.Teachers.Any())
            {
                Add(new Teacher { Name = "Teacher1", Email = "gmail.com" });
                Add(new Teacher { Name = "Teacher2", Email = "gmail.com" });
                Add(new Teacher { Name = "Teacher3", Email = "gmail.com" });
            }
        }

        public void Add(Teacher item) {
            _context.Teachers.Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<Teacher> GetAll() {
            return _context.Teachers.ToList();
        }

        public Teacher Find(long key) {
            throw new NotImplementedException();
        }

        public void Remove(long key) {
            throw new NotImplementedException();
        }

        public void Update(Teacher item) {
            throw new NotImplementedException();
        }
    }
}


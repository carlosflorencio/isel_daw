using System;
using System.Collections.Generic;
using System.Linq;
using _1617_2_LI41N_G9.Models;

namespace _1617_2_LI41N_G9.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(User item) {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll() {
            return _context.Students.ToList().Concat<User>(_context.Teachers.ToList());
        }

        public User Find(long key) {
            throw new NotImplementedException();
        }

        public void Remove(long key) {
            throw new NotImplementedException();
        }

        public void Update(User item) {
            throw new NotImplementedException();
        }
    }
}

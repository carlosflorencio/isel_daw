using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<bool> Add(User item) {
            throw new NotImplementedException();
        }
        
        public IEnumerable<User> GetAll(Func<User, bool> pred = default(Func<User, bool>))
        {
            if (pred == null)
                pred = u => { return true; };
                
            return _context.Students.ToList()
                .Concat<User>(_context.Teachers.ToList())
                .Where(pred);
        }

        public User Find(int Id) {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int Id) {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User item) {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1617_2_LI41N_G9.Models;

namespace _1617_2_LI41N_G9.Data.Repositories
{
    public class TeacherRepository : IRepository<Teacher>
    {
        private readonly DatabaseContext _context;

        public TeacherRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Teacher item) {
            await _context.Teachers.AddAsync(item);
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }

        public IEnumerable<Teacher> GetAll() {
            return _context.Teachers.ToList();
        }

        public Teacher Find(int Id) {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int Id) {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Teacher item) {
            throw new NotImplementedException();
        }
    }
}


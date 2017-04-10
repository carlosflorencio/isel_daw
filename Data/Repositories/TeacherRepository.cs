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

        public IEnumerable<Teacher> GetAll() {
            return _context.Teachers.ToList();
        }

        public IEnumerable<Teacher> GetAll(Func<Teacher, bool> pred = default(Func<Teacher, bool>))
        {
            if (pred == null)
                pred = u => { return true; };
                
            return _context.Teachers.Where(pred).ToList();
        }

        public async Task<bool> Add(Teacher item) {
            await _context.Teachers.AddAsync(item);
            if(await _context.SaveChangesAsync() > 0){
                _context.Entry(item).GetDatabaseValues();
                return true;
            }
            return false;
        }

        public Teacher Find(int Id) {
            return _context.Teachers.FirstOrDefault(t => t.Id == Id);
        }

        public async Task<bool> Remove(int Id) {
            var entity = _context.Teachers.FirstOrDefault(t => t.Id == Id);
            if(entity == null){
                return false;
            }
            _context.Teachers.Remove(entity);
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }

        public async Task<bool> Update(Teacher item) {
            _context.Teachers.Update(item);
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }
    }
}


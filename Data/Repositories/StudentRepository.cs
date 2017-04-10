using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1617_2_LI41N_G9.Models;

namespace _1617_2_LI41N_G9.Data.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly DatabaseContext _context;

        public StudentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public IEnumerable<Student> GetAll(Func<Student, bool> pred = default(Func<Student, bool>))
        {
            if (pred == null)
                pred = u => { return true; };
                
            return _context.Students.Where(pred).ToList();
        }

        public async Task<bool> Add(Student item)
        {
            var entity = await _context.Students.AddAsync(item);
            if(await _context.SaveChangesAsync() > 0){
                _context.Entry(item).GetDatabaseValues();
                return true;
            }
            return false;
        }

        public Student Find(int Id)
        {
            return _context.Students.FirstOrDefault(t => t.Id == Id);
        }

        public async Task<bool> Remove(int Id)
        {
            var entity = _context.Students.FirstOrDefault(t => t.Id == Id);
            if(entity == null){
                return false;
            }
            _context.Students.Remove(entity);
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }

        public async Task<bool> Update(Student item)
        {
            _context.Students.Update(item);
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }
    }
}

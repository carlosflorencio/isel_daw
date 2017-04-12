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

        public Task<IEnumerable<Student>> GetAll(Func<Student, bool> pred = default(Func<Student, bool>))
        {
            return Task.Factory.StartNew<IEnumerable<Student>>(() => {
                if (pred == null)
                    pred = u => { return true; };
                    
                return _context.Students.Where(pred).ToList();
            });
        }

        public async Task<Student> Find(int Id)
        {
            return await _context.Students.FindAsync(Id);
        }

        public async Task<bool> Add(Student item)
        {
            _context.Students.Add(item);       //No access to Database
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }

        public async Task<bool> Remove(int Id)
        {
            if(await Find(Id) == null){
                return false;
            }
            _context.Students.Remove(new Student { Id = Id });   //No access to Database
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }

        public async Task<bool> Update(Student item)
        {
            _context.Students.Update(item);     //No access to Database
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }
    }
}

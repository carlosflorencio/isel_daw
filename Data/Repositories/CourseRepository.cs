using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1617_2_LI41N_G9.Models;

namespace _1617_2_LI41N_G9.Data.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly DatabaseContext _context;

        public CourseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public IEnumerable<Course> GetAll(Func<Course, bool> pred = default(Func<Course, bool>))
        {
            if (pred == null)
                pred = u => { return true; };
                
            return _context.Courses.Where(pred).ToList();
        }

        public Course Find(int Id)
        {
            return _context.Courses.FirstOrDefault(t => t.Id == Id);
        }

        public async Task<bool> Add(Course item)
        {
            var entity = await _context.Courses.AddAsync(item);
            if(await _context.SaveChangesAsync() > 0){
                _context.Entry(item).GetDatabaseValues();
                return true;
            }
            return false;
        }

        public async Task<bool> Update(Course item)
        {
            _context.Courses.Update(item);
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }

        public async Task<bool> Remove(int Id)
        {
            var entity = _context.Courses.FirstOrDefault(t => t.Id == Id);
            if(entity == null){
                return false;
            }
            _context.Courses.Remove(entity);
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }
    }
}
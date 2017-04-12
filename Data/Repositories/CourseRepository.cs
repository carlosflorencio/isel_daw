using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public Task<IEnumerable<Course>> GetAll(Func<Course, bool> pred = default(Func<Course, bool>))
        {
            return Task.Factory.StartNew<IEnumerable<Course>>(() => {
                if (pred == null)
                    pred = u => { return true; };

                return _context.Courses
                    .Include(c => c.Coordinator)
                    .Include(c => c.Classes)
                    .Where(pred)
                    .ToList();
            });
        }

        public async Task<Course> Find(int Id)
        {
            return await _context.Courses
                .Where(c => c.Id == Id)
                .Include(c => c.Coordinator)
                .Include(c => c.Classes)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Add(Course item)
        {
            var entity = _context.Courses.Add(item);
            if(await _context.SaveChangesAsync() > 0){
                //item.Coordinator = await _context.Teachers.FindAsync(item.CoordinatorId);
                // Do the same for classes
                item = await this.Find(item.Id);     // Good? I don't think so
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
            if(await Find(Id) == null){
                return false;
            }
            _context.Courses.Remove(new Course { Id = Id });
            if(await _context.SaveChangesAsync() > 0){
                return true;
            }
            return false;
        }
    }
}
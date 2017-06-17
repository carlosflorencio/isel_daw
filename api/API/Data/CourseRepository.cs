using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CourseRepository : GenericRepository<DatabaseContext, Course>, ICourseRepository
    {
        public CourseRepository(DatabaseContext ctx) : base(ctx)
        {
        }

        public Task<PagedList<Course>> GetAllPaginatedAsync(ListQueryStringDto p)
        {
            IQueryable<Course> courses = Context.Courses
                .Include(c => c.Coordinator);

            //TODO: search by number
            if (!string.IsNullOrEmpty(p.Search))
            {
                var query = p.Search.Trim();

                courses = courses.Where(c => c.Name.Contains(query));
            }

            return PagedList<Course>.Create(courses, p.Page, p.Limit);
        }

        public Task<Course> GetByIdAsync(int Id)
        {
            return Context.Courses
                .Where(c => c.Id == Id)
                .Include(c => c.Coordinator)
                .Include(c => c.Classes)
                .FirstOrDefaultAsync();
        }

        public Task<PagedList<Class>> GetCourseClassesAsync(int id, ListQueryStringDto query)
        {
            IQueryable<Class> classes = Context.Classes
                .Where(c => c.CourseId == id)
                .Include(c => c.Course)
                .Include(c => c.Semester)
                .Include(c => c.Groups)
                .OrderBy(c => c.Semester);

            return PagedList<Class>.Create(classes, query.Page, query.Limit);
        }
    }
}
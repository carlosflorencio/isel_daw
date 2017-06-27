using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StudentRepository : GenericRepository<DatabaseContext, Student>, IStudentRepository
    {
        public StudentRepository(DatabaseContext ctx) : base(ctx)
        {
        }

        public override Task<List<Student>> GetAllAsync()
        {
            return Context.Students
                .Include(s => s.Groups)
                .Include(s => s.Classes)
                .ToListAsync();
        }

        public Task<PagedList<Student>> GetAllPaginatedAsync(ListQueryStringDto p)
        {
            IQueryable<Student> stds = Context.Students.OrderBy(s => s.Number);

            if (!string.IsNullOrEmpty(p.Search))
            {
                var query = p.Search.Trim();

                int numberToSearch = -1;

                try
                {
                    numberToSearch = Int32.Parse(query);
                }
                catch (FormatException)
                {
                    // we dont want to search by number then..
                }

                stds = stds.Where(
                    s =>
                        s.Email.Contains(query) ||
                        s.Name.Contains(query)  ||
                        s.Number == numberToSearch
                );
            }

            return PagedList<Student>.Create(stds, p.Page, p.Limit);
        }

        public Task<Student> GetByEmailAndPasswordAsync(string email, string password) {

            return Context.Students
                .Where(s => s.Email == email && s.Password == password)
                .SingleOrDefaultAsync();

        }

        public Task<Student> GetByNumberAsync(int id)
        {
            return Context.Students
                .Where(c => c.Number == id)
                .Include(c => c.Classes)
                .SingleOrDefaultAsync();
        }

        public Task<PagedList<Class>> GetStudentClasses(int number, ListQueryStringDto query)
        {
            IQueryable<Class> classes = Context.Classes
                .Where(cl => cl.Participants
                    .Where(cs => cs.StudentNumberId == number)
                    .FirstOrDefault() != default(ClassStudent)
                ).Include(cl => cl.Semester);

            return PagedList<Class>.Create(classes, query.Page, query.Limit);
        }
    }
}

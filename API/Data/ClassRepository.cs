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
    public class ClassRepository : GenericRepository<DatabaseContext, Class>, IClassRepository
    {
        public ClassRepository(DatabaseContext ctx) : base(ctx)
        {
        }

        public Task<Class> GetByIdAsync(int Id)
        {
            return Context.Classes
                .Where(c => c.Id == Id)
                .Include(c => c.Semester)
                .Include(c => c.Course)
                .FirstOrDefaultAsync();
        }

        public Task<PagedList<Class>> GetAllPaginatedAsync(ListQueryStringDto p)
        {
            IQueryable<Class> classes = Context.Classes
                .Include(c => c.Semester)
                .OrderBy(s => s.Id);

            if (!string.IsNullOrEmpty(p.Search))
            {
                var query = p.Search.Trim();

                int yearToSearch = -1;

                try
                {
                    yearToSearch = Int32.Parse(query);
                }
                catch (FormatException)
                {
                    // we dont want to search by number then..
                }

                classes = classes.Where(
                    s =>
                        s.Semester.Year == yearToSearch ||  //TODO: maybe add string 1617V?
                        s.Name.Contains(query)
                );
            }

            return PagedList<Class>.Create(classes, p.Page, p.Limit);
        }
    }
}
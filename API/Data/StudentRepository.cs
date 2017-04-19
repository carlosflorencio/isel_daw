using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StudentRepository : GenericRepository<DatabaseContext, Student>, IStudentRepository
    {
        public StudentRepository(DatabaseContext ctx) : base(ctx)
        {
        }

        public async Task<Student> GetByIdAsync(int id) {
            return await Context.Students.Where(c => c.Id == id).SingleOrDefaultAsync();
        }
    }
}

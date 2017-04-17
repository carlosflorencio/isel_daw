using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.Repositories.Contracts
{
    public interface IStudentRepository : IGenericRepository<Student> {

        Task<Student> GetByIdAsync(int Id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.Contracts
{
    public interface IStudentRepository : IGenericRepository<Student>
    {

        Task<Student> GetByIdAsync(int id);
    }
}

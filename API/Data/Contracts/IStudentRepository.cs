using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;

namespace API.Data.Contracts
{
    public interface IStudentRepository : IGenericRepository<Student>
    {

        Task<Student> GetByNumberAsync(int number);

        Task<PagedList<Student>> GetAllPaginatedAsync(ListQueryStringDto p);

        Task<Student> GetByEmailAndPasswordAsync(string email, string password);

    }
}

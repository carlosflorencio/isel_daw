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
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {

        Task<Teacher> GetByNumberAsync(int number);

        Task<PagedList<Teacher>> GetAllPaginatedAsync(ListQueryStringDto p);

        Task<Teacher> GetByEmailAndPasswordAsync(string email, string password);

        Task<PagedList<Class>> GetPaginatedTeacherClassesAsync(int number, ListQueryStringDto query);
        
        Task<PagedList<Course>> GetPaginatedTeacherCourses(int number, ListQueryStringDto query);
    }
    
}

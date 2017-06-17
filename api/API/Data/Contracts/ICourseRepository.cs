using System.Threading.Tasks;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;

namespace API.Data.Contracts
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course> GetByIdAsync(int Id);

        Task<PagedList<Course>> GetAllPaginatedAsync(ListQueryStringDto p);
    }
}
using System.Threading.Tasks;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;

namespace API.Data.Contracts
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<Class> GetByIdAsync(int Id);

        Task<PagedList<Class>> GetAllPaginatedAsync(ListQueryStringDto p);
    }
}
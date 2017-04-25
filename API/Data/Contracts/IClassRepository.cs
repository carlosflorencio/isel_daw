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

        Task<PagedList<Group>> GetClassGroups(int id, ListQueryStringDto p);
        
        Task<bool> AddParticipantTo(Class c, int studentNumberId);
    }
}
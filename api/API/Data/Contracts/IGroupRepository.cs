using System.Threading.Tasks;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;

namespace API.Data.Contracts
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        Task<Group> FindByIdAsync(int id);
    }
}
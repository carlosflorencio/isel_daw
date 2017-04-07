using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1617_2_LI41N_G9.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<bool> Add(T item);
        IEnumerable<T> GetAll();
        T Find(int Id);
        Task<bool> Remove(int Id);
        Task<bool> Update(T item);
    }
}

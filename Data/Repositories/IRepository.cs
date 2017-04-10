using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1617_2_LI41N_G9.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll(Func<T, bool> pred = default(Func<T, bool>));
        Task<T> Find(int Id);
        Task<bool> Add(T item);
        Task<bool> Remove(int Id);
        Task<bool> Update(T item);
    }
}

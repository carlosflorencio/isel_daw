using System.Collections.Generic;

namespace _1617_2_LI41N_G9.Data.Repositories
{
    public interface IRepository<T>
    {
        void Add(T item);
        IEnumerable<T> GetAll();
        T Find(long key);
        void Remove(long key);
        void Update(T item);
    }
}

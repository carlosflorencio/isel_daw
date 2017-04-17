using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using API.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public abstract class GenericRepository<C, T> :
        IGenericRepository<T> where T : class where C : DbContext, new()
    {

        protected readonly C _context;

        protected GenericRepository(C ctx)
        {
            _context = ctx;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            return await SaveAsync();
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);

            return await SaveAsync();
        }

        public virtual async Task<bool> EditAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return await SaveAsync();
        }

        public virtual async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

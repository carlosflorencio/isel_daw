using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using API.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public abstract class GenericRepository<C, T> :
        IGenericRepository<T> where T : class where C : DbContext, new()
    {

        protected readonly C Context;

        protected GenericRepository(C ctx)
        {
            Context = ctx;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);

            return await SaveAsync();
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);

            return await SaveAsync();
        }

        public virtual async Task<bool> EditAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            return await SaveAsync();
        }

        public virtual async Task<bool> SaveAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }
    }
}

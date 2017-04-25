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

        public virtual Task<List<T>> GetAllAsync()
        {
            return Context.Set<T>().ToListAsync();
        }

        public virtual Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual Task<bool> AddAsync(T entity)
        {
            return Context
                .Set<T>()
                .AddAsync(entity)
                .ContinueWith(antecendent => SaveAsync())
                .Unwrap();
        }

        public virtual Task<bool> DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);

            return SaveAsync();
        }

        public virtual Task<bool> EditAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            return SaveAsync();
        }

        protected Task<bool> SaveAsync()
        {
            return Context
                .SaveChangesAsync()
                .ContinueWith(antecedent => antecedent.Result > 0);
        }
    }
}

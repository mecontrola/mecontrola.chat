using MeControla.Core.Data.Entities;
using MeControla.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeControla.Core.Repositories
{
    public abstract class BaseAsyncRepository<TEntity> : ContextRepository<TEntity>, IAsyncRepository<TEntity>
         where TEntity : class, IEntity
    {
        protected BaseAsyncRepository(IDbContext context, DbSet<TEntity> dbSet)
            : base(context, dbSet)
        { }

        public virtual async Task<long> Count()
            => await dbSet.LongCountAsync();

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> predicate)
            => await dbSet.LongCountAsync(predicate);

        public virtual async Task<TEntity> CreateAsync(TEntity obj)
        {
            await ApplyAlterContextAsync(dbSet => dbSet.Add(obj));

            return obj;
        }

        public virtual async Task<bool> UpdateAsync(TEntity obj)
        {
            Detach(obj, EntityState.Modified);

            return await ApplyAlterContextAsync(dbSet => dbSet.Update(obj));
        }

        public virtual async Task<bool> RemoveAsync(TEntity obj)
        {
            Detach(obj, EntityState.Deleted);

            return await ApplyAlterContextAsync(dbSet => dbSet.Remove(obj));
        }

        public virtual async Task<IList<TEntity>> FindAllPagedAsync(IPaginationFilter paginationFilter)
            => await FindAllPagedAsync(paginationFilter, null, null);

        public virtual async Task<IList<TEntity>> FindAllPagedAsync(IPaginationFilter paginationFilter, params Expression<Func<TEntity, object>>[] includeProperties)
            => await FindAllPagedAsync(paginationFilter, null, includeProperties);

        public virtual async Task<IList<TEntity>> FindAllPagedAsync(IPaginationFilter paginationFilter, Expression<Func<TEntity, bool>> predicate)
            => await FindAllPagedAsync(paginationFilter, predicate, null);

        public virtual async Task<IList<TEntity>> FindAllPagedAsync(IPaginationFilter paginationFilter, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
            => await dbSet.SetPagination(paginationFilter).SetIncludes(includeProperties).SetPredicate(predicate).ToListAsync();

        public virtual async Task<IList<TEntity>> FindAllAsync()
            => await FindAllAsync(null, null);

        public virtual async Task<IList<TEntity>> FindAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
            => await FindAllAsync(null, includeProperties);

        public virtual async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
            => await FindAllAsync(predicate, null);

        public virtual async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
            => await dbSet.SetIncludes(includeProperties).SetPredicate(predicate).ToListAsync();

        public virtual async Task<TEntity> FindAsync(Guid id)
            => await FindAsync(itm => itm.Id.Equals(id), null);

        public virtual async Task<TEntity> FindAsync(params Expression<Func<TEntity, object>>[] includeProperties)
            => await FindAsync(null, includeProperties);

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await FindAsync(predicate, null);

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
            => await dbSet.AsNoTracking().SetIncludes(includeProperties).Where(predicate).FirstOrDefaultAsync();

        public async Task<bool> ExistsAsync(Guid Id)
            => await ExistsAsync(itm => itm.Id.Equals(Id));

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await dbSet.AnyAsync(predicate);

        protected virtual void Detach(TEntity entity, EntityState entityState)
        {
            var local = dbSet.Local.FirstOrDefault(itm => itm.Id.Equals(entity.Id));
            if (local.Id != 0)
                context.Entry(local).State = EntityState.Detached;
            context.Entry(entity).State = entityState;
        }

        private async Task<bool> ApplyAlterContextAsync(Action<DbSet<TEntity>> action)
        {
            action(dbSet);

            return await context.SaveChangesAsync() > 0;
        }
    }
}
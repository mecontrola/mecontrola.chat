using MeControla.Core.Data.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeControla.Core.Repositories
{
    public interface IAsyncRepository<TEntity>
        where TEntity : class, IEntity
    {
        DatabaseFacade Database();
        Task<long> Count();
        Task<long> Count(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> FindAllPagedAsync(IPaginationFilter paginationFilter);
        Task<IList<TEntity>> FindAllPagedAsync(IPaginationFilter paginationFilter, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IList<TEntity>> FindAllPagedAsync(IPaginationFilter paginationFilter, Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> FindAllPagedAsync(IPaginationFilter paginationFilter, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IList<TEntity>> FindAllAsync();
        Task<IList<TEntity>> FindAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> FindAsync(Guid guid);
        Task<TEntity> FindAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> CreateAsync(TEntity obj);
        Task<bool> UpdateAsync(TEntity obj);
        Task<bool> RemoveAsync(TEntity obj);
        Task<bool> ExistsAsync(Guid guid);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
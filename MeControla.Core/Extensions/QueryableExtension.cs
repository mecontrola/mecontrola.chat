using MeControla.Core.Repositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MeControla.Core.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<TEntity> SetPagination<TEntity>(this IQueryable<TEntity> query, IPaginationFilter paginationFilter)
            => paginationFilter == null
             ? query
             : query.Skip(GetSkip(paginationFilter)).Take(paginationFilter.PageSize);

        private static int GetSkip(IPaginationFilter paginationFilter)
           => (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        public static IQueryable<TEntity> SetPredicate<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate)
            => predicate == null
             ? query
             : query.Where(predicate);

        public static IQueryable<TEntity> SetIncludes<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (includeProperties == null)
                return query;

            foreach (var includeProperty in includeProperties)
                query.Include(includeProperty);

            return query;
        }
    }
}
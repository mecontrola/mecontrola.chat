using System;
using System.Collections.Generic;
using System.Linq;

namespace MeControla.Core.Extensions
{
    public static class EnumerableExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static bool IsNotNullAndAny<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        public static IList<T> ToListOrNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.IsNullOrEmpty() ? null : enumerable.ToList();
        }

        public static IList<TResult> SelectToListOrNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source.IsNullOrEmpty())
            {
                return null;
            }

            return source.Select(selector).ToList();
        }

        public static IList<TResult> SelectToListOrNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source.IsNullOrEmpty())
            {
                return null;
            }

            return source.Select(selector).ToList();
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static IEnumerable<int> FindIndexAll<T>(this IEnumerable<T> data, Predicate<T> match)
        {
            var list = (List<T>)data;

            return Enumerable.Range(0, data.Count())
                             .Where(i => match(list[i]));
        }
    }
}
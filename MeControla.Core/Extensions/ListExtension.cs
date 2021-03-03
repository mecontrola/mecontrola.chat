using System.Collections.Generic;
using System.Linq;

namespace MeControla.Core.Extensions
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<T>(this IList<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static bool IsNotNullAndAny<T>(this IList<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        public static IList<T> ToListOrNull<T>(this IList<T> enumerable)
        {
            return enumerable.IsNullOrEmpty() ? null : enumerable.ToList();
        }
    }
}
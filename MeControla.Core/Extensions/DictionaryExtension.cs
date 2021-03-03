using System.Collections.Generic;
using System.Linq;

namespace MeControla.Core.Extensions
{
    public static class DictionaryExtension
    {
        public static bool HasAny<TKey, TValue>(this IDictionary<TKey, TValue> elm)
        {
            return elm != null && elm.Any();
        }
    }
}
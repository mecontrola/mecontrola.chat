using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

namespace MeControla.Core.Extensions.Newtonsoft
{
    public static class JObjectExtension
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static T ToAnonymousType<T>(this JObject source, T template)
        {
            return source.ToObject<T>();
        }

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static T ToAnonymousType<T>(this JObject source, T template, JsonSerializer jsonSerializer)
        {
            return source.ToObject<T>(jsonSerializer);
        }
    }
}
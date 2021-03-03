using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MeControla.Core.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserFromJwt(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims.Single(itm => itm.Type.Equals("id")).Value;
        }
    }
}
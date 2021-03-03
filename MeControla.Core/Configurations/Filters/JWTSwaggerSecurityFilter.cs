using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeControla.Core.Configurations.Filters
{
    internal class JWTSwaggerSecurityFilter : IOperationFilter
    {
        private const string DESCRIPTION_CODE_401 = "If not authorized to perform the action.";
        private const string DESCRIPTION_CODE_403 = "If be forbidded to perform the action.";
        private const string STATUS_CODE_401 = "401";
        private const string STATUS_CODE_403 = "403";
        private const string GRANT_TYPE_BEARER = "Bearer";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!IsAuthorize(context))
                return;

            operation.Responses.TryAdd(STATUS_CODE_401, new OpenApiResponse { Description = DESCRIPTION_CODE_401 });
            operation.Responses.TryAdd(STATUS_CODE_403, new OpenApiResponse { Description = DESCRIPTION_CODE_403 });

            var bearer = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = GRANT_TYPE_BEARER }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    { bearer , Array.Empty<string>() }
                }
            };
        }

        private static bool IsAuthorize(OperationFilterContext context)
            => HasInformedTypeAttribute<AuthorizeAttribute>(context);
        //    || HasInformedTypeAttribute<ApiAuthorizeAttribute>(context);

        private static bool HasInformedTypeAttribute<T>(OperationFilterContext context)
            => context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<T>().Any()
            || context.MethodInfo.GetCustomAttributes(true).OfType<T>().Any();
    }
}
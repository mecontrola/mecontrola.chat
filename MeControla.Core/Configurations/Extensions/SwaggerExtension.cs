using MeControla.Core.Configurations.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace MeControla.Core.Configurations.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerSettings(this IServiceCollection services, ISwaggerConfiguration swaggerConfiguration)
        {
            if (!swaggerConfiguration.IsEnabled())
                return;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerConfiguration.Version, new OpenApiInfo
                {
                    Version = swaggerConfiguration.Version,
                    Title = swaggerConfiguration.Title,
                    Description = swaggerConfiguration.Description
                });

                AddJwtAuthentication(c, swaggerConfiguration);

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerSettings(this IApplicationBuilder app, ISwaggerConfiguration swaggerConfiguration)
        {
            if (!swaggerConfiguration.IsEnabled())
                return;

            app.UseSwagger(options => { options.RouteTemplate = swaggerConfiguration.JsonRoute; });
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint(
                    swaggerConfiguration.UIEndpoint,
                    swaggerConfiguration.Description
                );
                opt.DocumentTitle = swaggerConfiguration.Title;
                opt.DefaultModelsExpandDepth(0);
                opt.RoutePrefix = swaggerConfiguration.RoutePrefix;
            });
        }

        private static void AddJwtAuthentication(SwaggerGenOptions c, ISwaggerConfiguration swaggerConfiguration)
        {
            if (!swaggerConfiguration.JWTAuthentication)
                return;

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            c.OperationFilter<JWTSwaggerSecurityFilter>();
        }
    }
}
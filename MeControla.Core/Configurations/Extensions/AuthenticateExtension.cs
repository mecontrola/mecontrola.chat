using MeControla.Core.Configurations.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MeControla.Core.Configurations.Extensions
{
    public static class AuthenticateExtension
    {
        private const string POLICY_NAME = "Bearer";

        public static void AddAuthenticationSettings(this IServiceCollection services, IJWTConfiguration jwtConfiguration, IJWTManager jwtManager)
        {
            if (!jwtConfiguration.IsEnabled())
                return;

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                //opt.RequireHttpsMetadata = false;
                //opt.Authority = "http://localhost:5000";
                opt.SaveToken = true;
                opt.TokenValidationParameters = jwtManager.GetTokenValidationParameters();
            });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(POLICY_NAME, GenerateAuthorizationPolicy());
            });
        }

        private static AuthorizationPolicy GenerateAuthorizationPolicy()
            => new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                                               .RequireAuthenticatedUser()
                                               .Build();

        public static void UseAuthenticationSettings(this IApplicationBuilder app,
                                                     ICorsConfiguration corsConfiguration,
                                                     IJWTConfiguration jwtConfiguration)
        {
            if (!corsConfiguration.IsEnabled() || !jwtConfiguration.IsEnabled())
                return;

            app.UseCors(builder => builder
                .WithOrigins(corsConfiguration.Origins)
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            //.AllowCredentials()
            );

            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
using MeControla.Chat.Core.Extensions;
using MeControla.Chat.Core.Middlewares.Chat;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MeControla.Core.Configurations.Extensions;

namespace MeControla.Chat.Server
{
    public class Startup
    {
        private const string ROUTE_NAME = "default";
        private const string ROUTE_PATTERN = "{controller=Home}/{action=Index}/{id?}";
        private const string ROUTE_ERROR_PATH = "/home/error";

        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddSingleton(configuration.GetAppConfiguration());

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(ROUTE_ERROR_PATH);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseWebSockets();
            app.UseMiddleware<ChatMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(ROUTE_NAME, ROUTE_PATTERN);
            });
        }
    }
}
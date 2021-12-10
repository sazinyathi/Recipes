using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Recipes.Utils;


namespace Recipes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //services.AddControllersWithViews();
            services.AddHttpClient();

            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.AddScoped<IClaimsTransformation, ClaimsTransformer>();
            // Load up custom services
            //StartupService.AddService(services);

            //Local service
            services.AddScoped<ILocalisationService, LocalisationService>();
            services.AddSession();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddAuthorization(p =>
            //{
            //    p.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            //    p.AddPolicy("RequireTrainerRole", policy => policy.RequireRole("Trainer"));
            //    p.AddPolicy("RequireTeamLeaderRole", policy => policy.RequireRole("Team Leader"));
            //    p.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}

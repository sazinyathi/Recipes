using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Recipes.WebApi.Interface;
using Recipes.WebApi.Repository;

namespace Recipes.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureTransient(this IServiceCollection services)
        {
            services.AddTransient<IRole, RoleRepository>();
            services.AddTransient<IUser, UserRepository>();
           
        }

        public static void ConfigureOutputFormatters(this IServiceCollection services)
        {
            services.AddControllers(opt => // or AddMvc()
            {
                // remove formatter that turns nulls into 204 - No Content responses
                // this formatter breaks Angular's Http response JSON parsing
                 opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            });
        }
    }
}

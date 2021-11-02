using JohaRepository.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace AspNetCoreResult.Startup
{
    public static class AspNetCoreBuilderExtensions
    {
        /// <summary>
        /// Add Core Builder add
        ///        services.AddSession();
        ///        services.AddHttpContextAccessor();
        ///        services.AddCors();
        ///        services.AddDistributedMemoryCache();
        /// </summary>
        /// <param name="services"></param>
        public static void AddCoreBuilder(this IServiceCollection services)
        {
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddDistributedMemoryCache();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureApp(this IApplicationBuilder app)
        {
            app.UseCors(builder => builder
               .WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader());
            app.UseCookiePolicy();
            app.UseSession();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionModel"></param>
        public static void ConnectionServerForErrorModel(ErrorConnection connectionModel)
        {

        }

    }
}

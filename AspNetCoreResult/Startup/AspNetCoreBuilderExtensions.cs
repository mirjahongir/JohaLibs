using JohaRepository.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using System.Threading;

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
            //  ResultLogic.HttpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            ResultLogic.HttpContext = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            // ResultLogic.HttpContext = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            //  app.UseMiddleware<TokenMiddleware>();
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
            if (connectionModel != null && connectionModel.CHeck())
            {
                ResultLogic.ConnectionExist = true;
                ResultLogic.Connection = connectionModel;
                StartTimer();
            }
            else
            {
                ResultLogic.ConnectionExist = false;
            }

        }

        private static void ChangeTime(object state)
        {

        }
        private static void StartTimer()
        {

            TimerCallback callback = new TimerCallback(ChangeTime);
            Timer timer = new Timer(callback, null, 0, 100000);
        }


    }

}

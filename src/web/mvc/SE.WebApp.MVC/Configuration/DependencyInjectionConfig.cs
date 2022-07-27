using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SE.WebApp.MVC.Extensions;
using SE.WebApp.MVC.Services;

namespace SE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IIdentityService, IdentityService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}

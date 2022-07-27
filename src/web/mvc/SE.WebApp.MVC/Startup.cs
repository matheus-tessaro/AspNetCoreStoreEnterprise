using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SE.WebApp.MVC.Configuration;

namespace SE.WebApp.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration();
            services.AddMvcConfiguration();
            services.RegisterServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => 
            app.UseMvcConfiguration(env);
    }
}

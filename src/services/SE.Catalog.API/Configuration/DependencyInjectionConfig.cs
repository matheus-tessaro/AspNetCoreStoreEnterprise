using Microsoft.Extensions.DependencyInjection;
using SE.Catalog.API.Data;
using SE.Catalog.API.Data.Repository;
using SE.Catalog.API.Models;

namespace SE.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}

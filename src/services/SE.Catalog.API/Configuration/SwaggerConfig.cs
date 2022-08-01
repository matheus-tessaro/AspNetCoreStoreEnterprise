using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace SE.Catalog.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "StoreEnterprise Catalog API",
                    Description = "Product/catalog handling",
                    Contact = new OpenApiContact
                    {
                        Name = "Matheus Tessaro",
                        Email = "notmyrealemail@outlook.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    },
                    Version = "v1"
                });
            });

        public static void UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreEnterprise Catalog API v1"));
            }
        }
    }
}

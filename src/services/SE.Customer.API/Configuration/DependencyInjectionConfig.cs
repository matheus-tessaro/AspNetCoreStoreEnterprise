using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SE.Core.Mediator;
using SE.Customers.API.Application.Commands;
using SE.Customers.API.Application.Events;
using SE.Customers.API.Data;
using SE.Customers.API.Data.Repository;
using SE.Customers.API.Models;

namespace SE.Customers.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<INotificationHandler<RegisteredCustomerEvent>, CustomerEventHandler>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomerContext>();
        }
    }
}

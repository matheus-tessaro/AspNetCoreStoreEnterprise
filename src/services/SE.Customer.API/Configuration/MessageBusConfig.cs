using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SE.Core.Utils;
using SE.Customers.API.Services;
using SE.MessageBus;

namespace SE.Customers.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CustomerRegistryIntegrationHandler>();
    }
}

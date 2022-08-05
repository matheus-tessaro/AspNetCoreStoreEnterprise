using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SE.Core.Utils;
using SE.MessageBus;

namespace SE.Identity.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
    }
}

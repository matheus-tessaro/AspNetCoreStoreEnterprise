using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SE.Core.Mediator;
using SE.Core.Messages.Integration;
using SE.Customers.API.Application.Commands;
using SE.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SE.Customers.API.Services
{
    public class CustomerRegistryIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public CustomerRegistryIntegrationHandler(IMessageBus messageBus, IServiceProvider serviceProvider)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void SetResponder()
        {
            _messageBus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request => await RegisterCustomer(request));
            _messageBus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object s, EventArgs e) => SetResponder();

        private async Task<ResponseMessage> RegisterCustomer(UserRegisteredIntegrationEvent message)
        {
            ValidationResult success;
            RegisterCustomerCommand command = new(message.Id, message.Name, message.Email, message.SocialSecurityNumber);

            using (var scope = _serviceProvider.CreateScope()) 
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                success = await mediator.Send(command);
            }

            return new(success);
        }
    }
}

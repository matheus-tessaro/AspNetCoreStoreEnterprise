using FluentValidation.Results;
using MediatR;
using SE.Core.Messages;
using SE.Customers.API.Application.Events;
using SE.Customers.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SE.Customers.API.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler, IRequestHandler<RegisterCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerCommandHandler(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<ValidationResult> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request.Invalid())
                return request.ValidationResult;

            Customer customer = new(request.Id, request.Name, request.Email, request.SocialSecurityNumber);

            if (await _customerRepository.GetBySocialSecurityNumber(request.SocialSecurityNumber) != null)
            {
                AddError("Social security number already in use!");
                return ValidationResult;
            }

            await _customerRepository.Create(customer, cancellationToken);
            customer.AddNotification(new RegisteredCustomerEvent(request.Id, request.Name, request.Email, request.SocialSecurityNumber));

            return await Commit(_customerRepository.UnitOfWork);
        }
    }
}

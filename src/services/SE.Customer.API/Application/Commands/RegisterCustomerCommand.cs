using FluentValidation;
using SE.Core.DomainObjects;
using SE.Core.Messages;
using System;

namespace SE.Customers.API.Application.Commands
{
    public class RegisterCustomerCommand : Command
    {
        public RegisterCustomerCommand(Guid id, string name, string email, string socialSecurityNumber)
        {
            AggregatedId = id;
            Id = id;
            Name = name;
            Email = email;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string SocialSecurityNumber { get; private set; }

        public override bool Invalid() => !new RegisterCustomerValidation().Validate(this).IsValid;
    }

    public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Invalid customer id");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Customer name is required");

            RuleFor(x => x.SocialSecurityNumber)
                .Must(HasValidSSN)
                .WithMessage("Invalid social security number");

            RuleFor(x => x.Email)
                .Must(HasValidEmail)
                .WithMessage("Invalid e-mail");
        }

        protected static bool HasValidSSN(string socialSecurityNumber) => SocialSecurityNumber.Validate(socialSecurityNumber);

        protected static bool HasValidEmail(string email) => Email.Validate(email);
    }
}

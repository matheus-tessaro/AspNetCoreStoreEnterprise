using SE.Core.Messages;
using System;

namespace SE.Customers.API.Application.Events
{
    public class RegisteredCustomerEvent : Event
    {
        public RegisteredCustomerEvent(Guid id, string name, string email, string socialSecurityNumber)
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
    }
}

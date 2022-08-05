using System;

namespace SE.Core.Messages.Integration
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public UserRegisteredIntegrationEvent(Guid id, string name, string email, string socialSecurityNumber)
        {
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

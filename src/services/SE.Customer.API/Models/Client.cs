using SE.Core.DomainObjects;
using System;

namespace SE.Customer.API.Models
{
    public class Client : Entity, IAggregateRoot
    {
        protected Client() { }

        public Client(Guid id, string name, string email, string socialSecurityNumber)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            SocialSecurityNumber = new SocialSecurityNumber(socialSecurityNumber);
            Deleted = false;
        }

        public bool Deleted { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public SocialSecurityNumber SocialSecurityNumber { get; private set; }
        public Address Address { get; private set; }

        public void ChangeEmail(string email) => Email = new Email(email);

        public void SetAddress(Address address) => Address = address;
    }
}

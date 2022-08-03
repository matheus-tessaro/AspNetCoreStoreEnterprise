using SE.Core.DomainObjects;
using System;

namespace SE.Client.API.Models
{
    public class Address : Entity
    {
        public Address(
            string street, 
            string number, 
            string neighborhood, 
            string additionalDetails, 
            string postalCode, 
            string city, 
            string state)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            AdditionalDetails = additionalDetails;
            PostalCode = postalCode;
            City = city;
            State = state;
        }

        public Guid ClientId { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string AdditionalDetails { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
    }
}

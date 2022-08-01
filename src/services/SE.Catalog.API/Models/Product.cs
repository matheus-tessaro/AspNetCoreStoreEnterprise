using SE.Core.DomainObjects;
using System;

namespace SE.Catalog.API.Models
{
    public class Product : Entity, IAggregateRoot
    {
        public bool Active { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

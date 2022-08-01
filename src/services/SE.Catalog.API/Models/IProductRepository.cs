using SE.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SE.Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);

        void Create(Product product);
        void Update(Product product);
    }
}

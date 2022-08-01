using Microsoft.EntityFrameworkCore;
using SE.Catalog.API.Models;
using SE.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SE.Catalog.API.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;
        public ProductRepository(CatalogContext context) => _context = context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Product>> GetAll() =>
            await _context.Product.AsNoTracking().ToListAsync();

        public async Task<Product> GetById(Guid id) =>
            await _context.Product.FindAsync(id);

        public async void Create(Product product) =>
            await _context.Product.AddAsync(product);

        public void Update(Product product) =>
            _context.Product.Update(product);

        public void Dispose() =>
            _context?.Dispose();
    }
}

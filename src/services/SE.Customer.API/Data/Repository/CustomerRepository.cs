using Microsoft.EntityFrameworkCore;
using SE.Core.Data;
using SE.Customers.API.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SE.Customers.API.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        public CustomerRepository(CustomerContext context) => _context = context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Customer>> GetAll() =>
            await _context.Customers.AsNoTracking().ToListAsync();

        public async Task<Customer> GetBySocialSecurityNumber(string socialSecurityNumber) =>
            await _context.Customers.FirstOrDefaultAsync(c => c.SocialSecurityNumber.Number == socialSecurityNumber);

        public async Task Create(Customer customer, CancellationToken cancellationToken) =>
            await _context.Customers.AddAsync(customer, cancellationToken);

        public void Dispose() => _context.Dispose();
    }
}
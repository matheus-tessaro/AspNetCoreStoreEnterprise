using SE.Core.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SE.Customers.API.Models
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetBySocialSecurityNumber(string socialSecurityNumber);
        Task Create(Customer customer, CancellationToken cancellationToken);
    }
}

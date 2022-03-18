using System.Linq.Expressions;
using Video.Domain.Entities;

namespace Video.Application.Interfaces;

public interface ICustomerRepository : IAsyncGenericRepository<Customer>
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync(params Expression<Func<Customer, object>>[] includeProperties);
    Task<Customer> GetCustomerByIdAsync(Guid id);
    Task<Customer> DeleteCustomerByIdAsync(Guid id);
}

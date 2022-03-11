using System.Linq.Expressions;
using Video.Domain;

namespace Video.Application.Interfaces;

public interface ICustomerRepository : IAsyncGenericRepository<Customer>
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync(params Expression<Func<Customer, object>>[] includeProperties);
}

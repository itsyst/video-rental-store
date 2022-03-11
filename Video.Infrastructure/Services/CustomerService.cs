using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Video.Application.Interfaces;
using Video.Domain;
using Video.Infrastructure.Persistence;

#nullable disable

namespace Video.Infrastructure.Services;

public class CustomerService : BaseService<Customer>, ICustomerRepository
{
    public ApplicationDbContext _context;
    public CustomerService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync(params Expression<Func<Customer, object>>[] includeProperties)
    {
        IQueryable<Customer> query = _table;
        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties)
            {
                query = query.Include(includeProp);
            }

        }

        return await query.ToListAsync();
    }
}

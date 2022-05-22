using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Video.Application.Interfaces;
using Video.Domain.Entities;
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

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync(Expression<Func<Customer, bool>>? filter = null, params Expression<Func<Customer, object>>[] includeProperties)
    {
        IQueryable<Customer> query = _table;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties)
            {
                query = query.Include(includeProp);
            }

        }

        return await query.ToListAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(Guid id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<Customer> DeleteCustomerByIdAsync(Guid id)
    {
        Customer existing = await _table.FindAsync(id);
        if (existing != null)
        {
            _table.Remove(existing);
        }

        await _context.SaveChangesAsync();

        return await Task.FromResult(existing);
    }
}

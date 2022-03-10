using Microsoft.EntityFrameworkCore;
using Video.Application.Interfaces;
using Video.Infrastructure.Persistence;

namespace Video.Infrastructure.Services;

public class BaseService<T> : IAsyncGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    internal readonly DbSet<T> _table;

    public BaseService(ApplicationDbContext context)
    {
        _context = context;
        _table = context.Set<T>();    
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _table.ToListAsync();
    }
}

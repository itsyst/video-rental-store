using Microsoft.EntityFrameworkCore;
using Video.Application.Interfaces;
using Video.Infrastructure.Persistence;

#nullable disable

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

    public async Task<T> GetByIdAsync(int id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _table.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
         _table.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return entity;
    }
}

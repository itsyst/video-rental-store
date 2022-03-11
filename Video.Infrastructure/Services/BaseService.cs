﻿using Microsoft.EntityFrameworkCore;
using Video.Application.Interfaces;
using Video.Infrastructure.Persistence;

#nullable disable

namespace Video.Infrastructure.Services;

public class BaseService<T> : IAsyncGenericRepository<T> where T : class
{
#pragma warning disable IDE0052 
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

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _table.FindAsync(id);
    }
}

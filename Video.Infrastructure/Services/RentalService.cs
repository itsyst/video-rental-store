﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Video.Application.Interfaces;
using Video.Domain.Entities;
using Video.Infrastructure.Persistence;

namespace Video.Infrastructure.Services;

public class RentalService : BaseService<Rental>, IRentalRepository
{
    public ApplicationDbContext _context;
    public RentalService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rental>> GetAllRentalsAsync(params Expression<Func<Rental, object>>[] includeProperties)
    {
        IQueryable<Rental> query = _table;
        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties)
            {
                query = query.Include(includeProp);
            }
        }

        return await query.ToListAsync();
    }
 
    public async Task<Rental> GetRentalByIdAsync(int id)
    {
        return await _table.FindAsync(id);
    }

 
    public async Task<Rental> DeleteRentalByIdAsync(int id)
    {
        Rental existing = await _table.FindAsync(id);
        if (existing != null)
        {
            _table.Remove(existing);
        }

        await _context.SaveChangesAsync();

        return await Task.FromResult(existing);
    }
}

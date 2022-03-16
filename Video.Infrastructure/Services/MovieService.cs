using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Video.Application.Interfaces;
using Video.Domain.Entities;
using Video.Infrastructure.Persistence;

namespace Video.Infrastructure.Services;

public class MovieService : BaseService<Movie>, IMovieRepository
{
    public ApplicationDbContext _context;
    public MovieService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync(params Expression<Func<Movie, object>>[] includeProperties)
    {
        IQueryable<Movie> query = _table;
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

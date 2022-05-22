using System.Linq.Expressions;
using Video.Domain.Entities;

namespace Video.Application.Interfaces;

public interface IMovieRepository : IAsyncGenericRepository<Movie>
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync(Expression<Func<Movie, bool>>? filter = null, params Expression<Func<Movie, object>>[] includeProperties);
}

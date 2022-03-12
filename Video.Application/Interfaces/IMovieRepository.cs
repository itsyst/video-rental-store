using System.Linq.Expressions;
using Video.Domain;

namespace Video.Application.Interfaces;

public interface IMovieRepository : IAsyncGenericRepository<Movie>
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync(params Expression<Func<Movie, object>>[] includeProperties);
}

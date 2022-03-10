using Video.Domain;

namespace Video.Application.Interfaces;

public interface IMovieRepository : IAsyncGenericRepository<Movie>
{
}

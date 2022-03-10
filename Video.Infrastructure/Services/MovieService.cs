using Video.Application.Interfaces;
using Video.Domain;
using Video.Infrastructure.Persistence;

namespace Video.Infrastructure.Services;

public class MovieService : BaseService<Movie>, IMovieRepository
{
    public MovieService(ApplicationDbContext context) : base(context)
    {
    }
}

using Video.Application.Interfaces;
using Video.Domain.Entities;
using Video.Infrastructure.Persistence;

#nullable disable

namespace Video.Infrastructure.Services;

public class GenreService : BaseService<Genre>, IGenreRepository
{
    private readonly ApplicationDbContext _context;
    public GenreService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}

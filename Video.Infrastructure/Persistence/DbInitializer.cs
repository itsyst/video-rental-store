using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Video.Application.Interfaces;
using Video.Domain;

#nullable disable

namespace Video.Infrastructure.Persistence;

public class DbInitializer : IDbInitializer
{
    private readonly ILogger<DbInitializer> _logger;
    private readonly ApplicationDbContext _context;

    public DbInitializer(
        ILogger<DbInitializer> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void Initialize()
    {
        //Run if migrations are not applied
        try
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
        }

        // Create Movies
        if (!_context.Movies.Any())
        {
            _context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie(){
                            Id = Guid.Parse("{9b2457cc-a010-11ec-b909-0242ac120002}"),
                            Name = "The Last Mountain",
                            ImageUrl = "\\uploads\\posters\\6274541645284724.jpg"
                        },
                        new Movie(){
                            Id = Guid.Parse("{af4c2e9b-7427-4476-b5be-0f574b4d1a0c}"),
                            Name = "Restless",
                            ImageUrl = "\\uploads\\posters\\14184894844178489719.jpg"
                        },
                     });
            _context.SaveChanges();
        }

        // Create Customers
        if (!_context.Customers.Any())
        {
            _context.Customers.AddRange(new List<Customer>()
                    {
                        new Customer(){
                            Id = Guid.Parse("{3b21b690-4627-4e29-9c98-1d338e41d6f0}"),
                            FirstName = "Rhea",
                            LastName = "McLaughlin"
                        },
                        new Customer(){
                            Id = Guid.Parse("{9c434504-4a9f-4d22-978b-2b7e1216d206}"),
                            FirstName = "Eric",
                            LastName = "Ullrich"
                        },
                     });
            _context.SaveChanges();
        }
    }
}

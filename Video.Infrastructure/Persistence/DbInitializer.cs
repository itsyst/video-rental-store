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

        // Create MembershipTypes
        if (!_context.MembershipTypes.Any())
        {
            _context.MembershipTypes.AddRange(new List<MembershipType>()
                    {
                        new MembershipType(){
                            Id=1,
                            SignUpFee = 0,
                            Duration = 0 ,
                            DiscountRate = 0
                        },
                        new MembershipType(){
                            Id=2,
                            SignUpFee = 25,
                            Duration = 1,
                            DiscountRate = 5
                        },
                        new MembershipType(){
                            Id=3,
                            SignUpFee = 255,
                            Duration = 12,
                            DiscountRate = 15
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
                    LastName = "McLaughlin",
                    IsSubscribed = true,
                    MembershipTypeId = 1,
                    Birthdate = new DateTime(1980,02,28)
                },
                new Customer(){
                    Id = Guid.Parse("{9c434504-4a9f-4d22-978b-2b7e1216d206}"),
                    FirstName = "Eric",
                    LastName = "Ullrich",
                    IsSubscribed = false,
                    MembershipTypeId = 2,
                    Birthdate = new DateTime(1978,08,06)
                },
                new Customer(){
                    Id = Guid.Parse("{896dd529-d60f-4fca-ac6e-df22807428d1}"),
                    FirstName = "Joe",
                    LastName = "Doe",
                    IsSubscribed = true,
                    MembershipTypeId = 3,
                    Birthdate = new DateTime(1969,05,14)
                },
            });
            _context.SaveChanges();
        }

        // Create Genres
        if (!_context.Genres.Any())
        {
            _context.Genres.AddRange(new List<Genre>()
            {
                new Genre(){
                    Name = "Documentary"
                },
                new Genre(){
                    Name = "Action"
                },
                new Genre(){
                    Name = "Crime"
                },
                new Genre(){
                    Name = "Thriller"
                },
                new Genre(){
                    Name = "Horror"
                },
                new Genre(){
                    Name = "Drama"
                },
                new Genre(){
                    Name = "Romance"
                },
                new Genre(){
                    Name = "Adventure"
                },
                new Genre(){
                    Name = "Comedy"
                },
                new Genre(){
                    Name = "Science Fiction"
                },
                new Genre(){
                    Name = "Detective"
                },
                new Genre(){
                    Name = "Classic"
                },
             });
            _context.SaveChanges();
        }
        // Create Movies
        if (!_context.Movies.Any())
        {
            _context.Movies.AddRange(new List<Movie>()
            {
                new Movie(){
                    Name = "The Last Mountain",
                    ImageUrl = "\\uploads\\posters\\6274541645284724.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2021, 09,09),
                    GenreId = 1,
                    InStock = 9
                },
                new Movie(){
                    Name = "Restless",
                    ImageUrl = "\\uploads\\posters\\14184894844178489719.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,02,25),
                    GenreId = 2,
                    InStock = 5
                },
                new Movie(){
                    Name = "The Batman",
                    ImageUrl = "\\uploads\\posters\\65196419850968199081298.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,02,22),
                    GenreId = 3,
                    InStock = 8
                },
                new Movie(){
                    Name = "The Commando",
                    ImageUrl = "\\uploads\\posters\\1684918424984984984849.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,01,07),
                    GenreId = 4,
                    InStock = 9
                },
                new Movie(){
                    Name = "Scream",
                    ImageUrl = "\\uploads\\posters\\8619814984984844.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,01,14),
                    GenreId = 5,
                    InStock = 3
                },
            });
            _context.SaveChanges();
        }
    }
}

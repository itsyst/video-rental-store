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
                            Id = Guid.NewGuid(),
                            Name = "The Last Mountain",
                            ImageUrl = "\\uploads\\posters\\6274541645284724.jpg",
                            CreatedDate = DateTime.Now,
                            ReleaseDate = new DateTime(2021, 09,09),
                            Genre = new Genre()
                            {
                                Id =  new Guid("4257d3b8-127c-4e0c-aa98-e0ae1aa64cd2"),
                                Name = "Documentary"
                            },
                            GenreId = Guid.Parse("{4257d3b8-127c-4e0c-aa98-e0ae1aa64cd2}"),
                            InStoch = 9
                        },
                        new Movie(){
                            Id = Guid.NewGuid(),
                            Name = "Restless",
                            ImageUrl = "\\uploads\\posters\\14184894844178489719.jpg",
                            CreatedDate = DateTime.Now,
                            ReleaseDate = new DateTime(2022,02,25),
                            Genre = new Genre(){
                                Id = new Guid("0be221e7-1d7d-4fbd-b99f-5fe52cdd575a"),
                                Name = "Action"
                            },
                            GenreId = Guid.Parse("{0be221e7-1d7d-4fbd-b99f-5fe52cdd575a}"),
                            InStoch = 5
                        },
                        new Movie(){
                            Id = Guid.NewGuid(),
                            Name = "The Batman",
                            ImageUrl = "\\uploads\\posters\\65196419850968199081298.jpg",
                            CreatedDate = DateTime.Now,
                            ReleaseDate = new DateTime(2022,02,22),
                            Genre = new Genre(){
                                Id = new Guid("f0a7b2d7-6b2d-48f5-85a9-24c43a3ef950"),
                                Name = "Crime"
                            },
                            GenreId =  Guid.Parse("{f0a7b2d7-6b2d-48f5-85a9-24c43a3ef950}"),
                            InStoch = 8
                        },
                        new Movie(){
                            Id =Guid.NewGuid(),
                            Name = "The Commando",
                            ImageUrl = "\\uploads\\posters\\1684918424984984984849.jpg",
                            CreatedDate = DateTime.Now,
                            ReleaseDate = new DateTime(2022,01,07),
                            Genre =  new Genre(){
                                Id = new Guid("f8a01819-efb4-4517-8c57-06c46b02981e"),
                                Name = "Thriller"
                            },
                           GenreId =   Guid.Parse("{f8a01819-efb4-4517-8c57-06c46b02981e}"),
                            InStoch = 9
                        },
                        new Movie(){
                            Id = Guid.NewGuid(),
                            Name = "Scream",
                            ImageUrl = "\\uploads\\posters\\8619814984984844.jpg",
                            CreatedDate = DateTime.Now,
                            ReleaseDate = new DateTime(2022,01,14),
                            Genre = new Genre(){
                                Id = new Guid("699da09f-6fd9-4abf-b5ca-69d714c68963"),
                                Name = "Horror"
                            },
                            GenreId =  Guid.Parse("{699da09f-6fd9-4abf-b5ca-69d714c68963}"),
                            InStoch = 3
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
                    Id = Guid.Parse("{c68412d5-a37c-4f4e-90c1-13dac34dd6d9}"),
                    Name = "Drama"
                },
                new Genre(){
                    Id = Guid.Parse("{af443f38-acbf-4f30-bdec-7311e9febcdc}"),
                    Name = "Romance"
                },
                new Genre(){
                    Id = Guid.Parse("{ba0c8873-5b1d-462b-adaa-9e9cb4825b47}"),
                    Name = "Adventure"
                },
                new Genre(){
                    Id = Guid.Parse("{bb46feb4-08d6-436d-a43b-b2f5116da511}"),
                    Name = "Comedy"
                },
                new Genre(){
                    Id = Guid.Parse("{c7be5594-b0cc-464e-8b13-c176ffa2ef85}"),
                    Name = "Science Fiction"
                },
                new Genre(){
                    Id = Guid.Parse("{dae6664c-9d8d-484e-ac02-ce426c061059}"),
                    Name = "Detective"
                },
                new Genre(){
                    Id = Guid.Parse("{4257d3b8-127c-4e0c-aa98-e0ae1aa64cd2}"),
                    Name = "Documentary"
                },
                new Genre(){
                    Id = Guid.Parse("{4b286382-bad5-4aaf-b106-fc71e7bface9}"),
                    Name = "Classic"
                },
            });
            _context.SaveChanges();
        }

        // Create MembershipTypes
        if (!_context.MembershipTypes.Any())
        {
            _context.MembershipTypes.AddRange(new List<MembershipType>()
                    {
                        new MembershipType(){
                            Id = 1,
                            SignUpFee = 0,
                            Duration = 0 ,
                            DiscountRate = 0
                        },
                        new MembershipType(){
                            Id = 2,
                            SignUpFee = 25,
                            Duration = 1,
                            DiscountRate = 5
                        },
                        new MembershipType(){
                            Id = 3,
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
    }
}

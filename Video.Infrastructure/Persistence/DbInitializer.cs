using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Video.Application.Interfaces;
using Video.Domain.Entities;
using Video.Domain.Utilities;

#nullable disable

namespace Video.Infrastructure.Persistence;

public class DbInitializer : IDbInitializer
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<DbInitializer> _logger;
    private readonly ApplicationDbContext _context;

    public DbInitializer(
                    RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
        ILogger<DbInitializer> logger,
        ApplicationDbContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
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

        if (!_roleManager.RoleExistsAsync(Roles.Admin).GetAwaiter().GetResult())
        {

            _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Guest)).GetAwaiter().GetResult();

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
                new Genre(){
                    Name = "Family"
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
                    InStock = 9,
                    Overview = "The compelling story of 30-year-old climber Tom Ballard who disappeared on one of the Himalayas' most deadly mountains, Nanga Parbat, in February 2019. Tom was the son of mountaineer Alison Hargreaves, who perished on K2 in 1995. "
                },
                new Movie(){
                    Name = "Restless",
                    ImageUrl = "\\uploads\\posters\\14184894844178489719.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,02,25),
                    GenreId = 2,
                    InStock = 5,
                    Overview = "After going to extremes to cover up an accident, a corrupt cop's life spirals out of control when he starts receiving threats from a mysterious witness."
                },
                new Movie(){
                    Name = "The Batman",
                    ImageUrl = "\\uploads\\posters\\65196419850968199081298.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,02,22),
                    GenreId = 3,
                    InStock = 8,
                    Overview = "In his second year of fighting crime, Batman uncovers corruption in Gotham City that connects to his own family while facing a serial killer known as the Riddler."
                },
                new Movie(){
                    Name = "The Commando",
                    ImageUrl = "\\uploads\\posters\\1684918424984984984849.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,01,07),
                    GenreId = 4,
                    InStock = 9,
                    Overview = "An elite DEA agent returns home after a failed mission when his family makes an unexpected discovery in their house – a stash of money worth $3 million. "
                },
                new Movie(){
                    Name = "Scream",
                    ImageUrl = "\\uploads\\posters\\8619814984984844.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,01,14),
                    GenreId = 5,
                    InStock = 3,
                    Overview = "Twenty-five years after a streak of brutal murders shocked the quiet town of Woodsboro, a new killer has donned the Ghostface mask and begins targeting a group of teenagers to resurrect secrets from the town’s deadly past."
                },
                new Movie(){
                    Name = "Blacklight",
                    ImageUrl = "\\uploads\\posters\\9eac3468-08dc-442e-8361-1c7b339b7486.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,02,10),
                    GenreId = 2,
                    InStock = 3,
                    Overview = "Travis Block is a shadowy Government agent who specializes in removing operatives whose covers have been exposed. He then has to uncover a deadly conspiracy within his own ranks that reaches the highest echelons of power."
                },
                new Movie(){
                    Name = "The Requin",
                    ImageUrl = "\\uploads\\posters\\32fcef1e-156d-4555-a904-a048a70bbdb7.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,01,28),
                    GenreId = 5,
                    InStock = 3,
                    Overview = "A couple on a romantic getaway find themselves stranded at sea when a tropical storm sweeps away their villa. In order to survive, they are forced to fight the elements, while sharks circle below."
                },
                new Movie(){
                    Name = "Pursuit",
                    ImageUrl = "\\uploads\\posters\\d20e271f-8f80-4eba-9238-4a011ab71697.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,02,18),
                    GenreId = 3,
                    InStock = 3,
                    Overview = "Detective Breslin crosses paths with Calloway, a ruthless hacker desperate to find his wife, who has been kidnapped by a drug cartel. When Calloway escapes police custody, Breslin joins forces with a no-nonsense female cop to reclaim his prisoner. "
                },
                new Movie(){
                    Name = "Finch",
                    ImageUrl = "\\uploads\\posters\\331fae2c-3821-4eaf-919b-3c36fff6d621.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2021,11,04),
                    GenreId = 10,
                    InStock = 3,
                    Overview = "On a post-apocalyptic Earth, a robot, built to protect the life of his dying creator's beloved dog, learns about life, love, friendship, and what it means to be human."
                },
                new Movie(){
                    Name = "Erax",
                    ImageUrl = "\\uploads\\posters\\47429501-b19c-4532-b42f-5a728443b97a.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2022,02,17),
                    GenreId = 13,
                    InStock = 3,
                    Overview = "Monstrous creatures leap from a magical storybook and unleash mayhem and mischief for Auntie Opal and her tween niece Nina in this spooky short film."
                },
                new Movie(){
                    Name = "Encounter",
                    ImageUrl = "\\uploads\\posters\\4bcdfeaa-4313-4e55-9450-43fdd8f7f1a8.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2021,12,03),
                    GenreId = 6,
                    InStock = 3,
                    Overview="A decorated Marine goes on a rescue mission to save his two young sons from an unhuman threat. As their journey takes them in increasingly dangerous directions, the boys will need to leave their childhoods behind."
                },
                new Movie(){
                    Name = "Don't Look Up",
                    ImageUrl = "\\uploads\\posters\\ca5155a0-796f-428c-b4c2-fb8eb931e7eb.jpg",
                    CreatedDate = DateTime.Now,
                    ReleaseDate = new DateTime(2021,12,07),
                    GenreId = 9,
                    InStock = 3,
                    Overview = "Two low-level astronomers must go on a giant media tour to warn humankind of an approaching comet that will destroy planet Earth."
                },
            });
            _context.SaveChanges();
        }
    }
}

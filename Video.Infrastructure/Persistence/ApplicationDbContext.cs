using Microsoft.EntityFrameworkCore;
using Video.Domain;

namespace Video.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Genre>? Genres { get; set; }
    public DbSet<Movie>? Movies { get; set; }
    public DbSet<MembershipType>? MembershipTypes { get; set; }
    public DbSet<Customer>? Customers { get; set; }
}

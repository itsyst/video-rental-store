using Microsoft.EntityFrameworkCore;
using Video.Domain;

namespace Video.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Movie>? Movies { get; set; }
    public DbSet<Customer>? Customers { get; set; }
}

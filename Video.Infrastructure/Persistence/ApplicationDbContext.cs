﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Video.Domain.Entities;

namespace Video.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(
        IConfiguration configuration,
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Genre>? Genres { get; set; }
    public DbSet<MembershipType>? MembershipTypes { get; set; }
    public DbSet<Customer>? Customers { get; set; }
    public DbSet<Movie>? Movies { get; set; }
    public DbSet<ApplicationUser>? Users { get; set; }
    public DbSet<Rental>? Rentals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"])
                      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.EnableSensitiveDataLogging();


    }
}

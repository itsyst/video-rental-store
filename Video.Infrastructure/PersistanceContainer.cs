﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Video.Application.Interfaces;
using Video.Infrastructure.Persistence;
using Video.Infrastructure.Services;

namespace Video.Infrastructure;

public static class PersistanceContainer
{
    //Services configuration
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
        );

        services.AddScoped<IMovieRepository, MovieService>();
        services.AddScoped<ICustomerRepository, CustomerService>();
        services.AddScoped<IGenreRepository, GenreService>();
        services.AddScoped<IRentalRepository, RentalService>();

        services.AddScoped<IDbInitializer, DbInitializer>();

        return services;
    }

    // Seed Database
    public static void SeedDatabase(AsyncServiceScope service)
    {
        using var scope = service;
        var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        initializer.Initialize();
    }
}

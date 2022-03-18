using Microsoft.Extensions.DependencyInjection;

namespace Video.Application;

public static class ApplicationContainer
{
    //Services configuration
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace SGMC.Test.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMediatR(congig => 
            congig.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}

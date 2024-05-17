using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using SGMC.Test.Application.Common.Behaviors;

namespace SGMC.Test.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMediatR(congig => 
            congig.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}

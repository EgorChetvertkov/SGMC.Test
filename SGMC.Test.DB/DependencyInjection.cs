using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SGMC.Test.DB;
public static class DependencyInjection
{
    public const string ConnectionStringNameDefault = "DefaultConnectionString";

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(ConnectionStringNameDefault)));

        return services;
    }
}

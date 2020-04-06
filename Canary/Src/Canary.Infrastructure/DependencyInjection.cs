using Canary.Application;
using Canary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Canary.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<CanaryDbContext>(options =>
            {
                options.UseSqlServer
                    (
                        connectionString: configuration.GetConnectionString("CanaryDbConstring"),
                        sqlServerOptionsAction: opt => opt.MigrationsAssembly("Canary.DbMigration")
                    );
            });

            service.AddScoped<ICanaryDbContext>(provider => provider.GetService<CanaryDbContext>());

            return service;

        }
    }
}

using Arcturus.Application;
using Arcturus.Infrastructure.Persistence;
using Arcturus.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Arcturus.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, 
            IConfiguration configuration, 
            ILoggerFactory loggerFactory = null)
        {
            service.AddDbContext<ArcturusDbContext>(options =>
            {
                options.UseSqlServer(
                    connectionString: configuration.GetConnectionString("ArcturusDbConstring"),
                    sqlServerOptionsAction: opt =>
                    {
                        opt.MigrationsAssembly("Arcturus.DbMigration");
                        opt.MigrationsHistoryTable("MigrationHistory", "Admin");
                    }
                );

                if (loggerFactory != null)
                {
                    options.UseLoggerFactory(loggerFactory);
                }
            });

            service.AddScoped<IArcturusDbContext>(provider => provider.GetService<ArcturusDbContext>());

            service.AddScoped<DbContext>(provider => provider.GetService<ArcturusDbContext>());

            return service;

        }
    }
}

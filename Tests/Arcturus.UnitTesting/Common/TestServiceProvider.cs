using Arcturus.Application;
using Arcturus.Infrastructure;
using Arcturus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Arcturus.UnitTesting.Common
{
    public class TestServiceProvider : IDisposable
    {
        private ServiceCollection serviceCollection = null;
        private ServiceProvider serviceProvider = null;

        public string DatabaseName { get; private set; }


        public static TestServiceProvider InSQLContext(Action<ServiceCollection> additionalServices = null)
        {
            return new TestServiceProvider(null, additionalServices);
        }

        public static TestServiceProvider InMemoryContext(Action<ServiceCollection> additionalServices = null)
        {
            string _dbName = $"db-{Guid.NewGuid().ToString().Substring(0, 8)}";

            return new TestServiceProvider(_dbName, additionalServices);
        }

        private TestServiceProvider(string dbName, Action<ServiceCollection> additionalServices = null)
        {
            DatabaseName = dbName;

            serviceCollection = new ServiceCollection();
            var _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            serviceCollection.AddSingleton<IConfiguration>(p => _config);

            serviceCollection.AddScoped<ICurrentAppUser, TestCurrentUser>();
            serviceCollection.AddScoped<IDateTime, TestDateTime>();

            serviceCollection.AddApplication();
            serviceCollection.AddLogging();

            if (string.IsNullOrWhiteSpace(dbName))
            {
                serviceCollection.AddInfrastructure(_config);
            }
            else
            {
                serviceCollection.AddDbContext<ArcturusDbContext>(opt =>
                {
                    opt.UseInMemoryDatabase(databaseName: DatabaseName);
                    opt.ConfigureWarnings(a => a.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

                serviceCollection.AddScoped<IArcturusDbContext>(provider => provider.GetService<ArcturusDbContext>());
            }

            if (additionalServices != null)
            {
                additionalServices.Invoke(serviceCollection);
            }

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                var _dbContext = GetService<DbContext>();

                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }

                serviceProvider.Dispose();

            }
        }
    }
}

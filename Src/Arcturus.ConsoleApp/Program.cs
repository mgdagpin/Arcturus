using Arcturus.Application;
using Arcturus.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using TasqR;

namespace Arcturus.ConsoleApp
{
    class Program
    {
        static readonly ILoggerFactory SampleLoggingFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var mainService = CreateHostBuilder(args).Build().Services;
        }

        static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration, SampleLoggingFactory);
            services.AddApplication();

            services.AddTasqR(Assembly.GetExecutingAssembly());

            services.AddMemoryCache();
        }

        #region Host Builder
        static AppServiceBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var builder = new AppServiceBuilder();

            ConfigureServices(builder.Services, config);

            return builder;
        }

        class AppServiceBuilder
        {
            public ServiceCollection Services { get; } = new ServiceCollection();
            public AppServiceProvider Build() => new AppServiceProvider(Services.BuildServiceProvider());
        }

        class AppServiceProvider
        {
            public AppServiceProvider(IServiceProvider services) { Services = services; }
            public IServiceProvider Services { get; }
        } 
        #endregion
    }

}

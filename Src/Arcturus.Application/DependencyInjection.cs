using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using TasqR;

namespace Arcturus.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, bool includeValidators = false)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTasqR(Assembly.GetExecutingAssembly());

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            // useful for unittesting
            if (includeValidators)
            {
                var _assembly = Assembly.GetExecutingAssembly();

                var _validatorTypes = _assembly.GetTypes()
                    .Where(a => a.BaseType != null
                             && a.BaseType.IsGenericType
                             && a.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
                    .Select(a => new
                    {
                        Validator = a,
                        IValidator = typeof(IValidator<>)
                            .MakeGenericType
                            (
                                a.BaseType.GetGenericArguments().Single()
                            )
                    })
                    .ToList();

                foreach (var _validator in _validatorTypes)
                {
                    services.AddTransient(_validator.IValidator, _validator.Validator);
                }
            }

            return services;
        }
    }
}


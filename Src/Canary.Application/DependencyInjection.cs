using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Canary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service, bool includeValidators = false)
        {
            service.AddMediatR(Assembly.GetExecutingAssembly());
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

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
                    service.AddTransient(_validator.IValidator, _validator.Validator);
                }
            }

            return service;
        }
    }
}


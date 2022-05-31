using AVEVA.PA.MicroserviceTemplate.Application.Commands;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microservice.Application.Validation;
using Microservice.Application.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace AVEVA.PA.MicroserviceTemplate.Application
{
    public static class StartupConfigurator
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            // register DI types here
            services.AddMediatR(typeof(CreateProjectCommand).Assembly);
            services.AddAutoMapper(typeof(CreateProjectDto).Assembly);

            // Add fluent Validation
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateProjectCommandValidator>());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMemoryCache();

            // add language for validation messages. if required commented for now
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");
        }
    }
}

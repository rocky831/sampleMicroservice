using AVEVA.PA.Exceptions;
using AVEVA.PA.MicroserviceTemplate.Web.Health;
using Microsoft.Extensions.DependencyInjection;

namespace AVEVA.PA.MicroserviceTemplate.Web
{
    public static class StartupConfigurator
    {
        public static void ConfigureWeb(this IServiceCollection services, string connectionString)
        {
            services.AddControllers().AddNewtonsoftJson();

            // Add Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<IErrorVocabularyFactory>(
                provider => new JsonErrorVocabularyFactory("Exceptions/Localization/")
            );

            services.AddHealthChecks()
                .AddSqlServer(connectionString)
                .AddCheck<MicroserviceTemplateHealthCheck>(nameof(MicroserviceTemplateHealthCheck));

            services.AddHealthChecksUI().AddInMemoryStorage();
        }
    }
}

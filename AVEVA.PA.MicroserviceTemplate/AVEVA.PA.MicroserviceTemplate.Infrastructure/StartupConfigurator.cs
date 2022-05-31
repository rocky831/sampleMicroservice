using MassTransit;
using AVEVA.PA.MicroserviceTemplate.Core;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using AVEVA.PA.MicroserviceTemplate.Core.IRepositories;
using AVEVA.PA.MicroserviceTemplate.Infrastructure.Data;
using AVEVA.PA.MicroserviceTemplate.Infrastructure.MessageConsumer;
using AVEVA.PA.MicroserviceTemplate.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AVEVA.PA.DataAccess;

namespace AVEVA.PA.MicroserviceTemplate.Infrastructure
{
    public static class StartupConfigurator
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.ConfigureCore();


            services.AddDbContextFactory<PaDbContext>(builder => builder
                     .UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProjectConsumer>();
 

                x.UsingRabbitMq((ctx, config) =>
                {
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                    config.ReceiveEndpoint("project-queue", c =>
                    {
                        c.ConfigureConsumer<ProjectConsumer>(ctx);
                    });
                });
            });             
        }
    }
}

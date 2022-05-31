using AVEVA.PA.MicroserviceTemplate.Application;
using AVEVA.PA.Exceptions;
using AVEVA.PA.MicroserviceTemplate.Infrastructure;
using AVEVA.PA.MicroserviceTemplate.Web;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using AVEVA.PA.Logging;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// add logger
builder.Host.UseSerilog(
    (context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDataEnricher()
        // The source is the name of current microservice
        .Enrich.WithProperty("Source", "MicroserviceTemplate")
        .Enrich.WithGuidLogIdEnricher()
        .Enrich.WithHttpRequestEnricher(Serilog.Events.LogEventLevel.Error));

// Set up modules
services.ConfigureInfrastructure(builder.Configuration.GetConnectionString("SqlDbConnection"));
services.ConfigureWeb(builder.Configuration.GetConnectionString("SqlDbConnection"));
services.ConfigureApplication(); 
var app = builder.Build(); 



// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseSerilogRequestLogging();

app.UseHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});
app.MapHealthChecksUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

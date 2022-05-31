using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AVEVA.PA.MicroserviceTemplate.Web.Health
{
    public class MicroserviceTemplateHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken()) =>
            //TODO: Implement health check logic here 
            await Task.Run(() => HealthCheckResult.Healthy(), cancellationToken);
    }
}

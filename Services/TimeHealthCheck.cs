using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LR7.Services
{
    public class TimeHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var isHealthy = DateTime.UtcNow.Minute % 2 == 0;
            if (isHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Системний час на парних хвилинах"));
            }
            return Task.FromResult(HealthCheckResult.Unhealthy("Системний час на непарних хвилинах"));
        }
    }
}

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LR7.Services
{
    public class FileSystemHealthCheck : IHealthCheck
    {
        private readonly string _filePath;

        public FileSystemHealthCheck(string filePath)
        {
            _filePath = filePath;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var fileExists = File.Exists(_filePath);
                if (fileExists)
                {
                    return Task.FromResult(HealthCheckResult.Healthy("Файл існує"));
                }
                return Task.FromResult(HealthCheckResult.Unhealthy("Файл не знайдено"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"Виникла помилка: {ex.Message}"));
            }
        }
    }
}

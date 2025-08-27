using Microsoft.Extensions.Diagnostics.HealthChecks;
namespace EFakturCallback.Middleware
{
	public class CustomHealthCheckMiddleware: IHealthCheck
	{

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(HealthCheckResult.Healthy("Healthy"));
            }
            catch
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "Unhealthy"));
            }
        }
    }
}


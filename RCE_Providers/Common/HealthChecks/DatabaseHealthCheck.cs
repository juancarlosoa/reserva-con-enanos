using Microsoft.Extensions.Diagnostics.HealthChecks;
using RCE_Providers.CoreData;

namespace RCE_Providers.Common.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly ProvidersDbContext _context;

    public DatabaseHealthCheck(ProvidersDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Database.CanConnectAsync(cancellationToken);
            return HealthCheckResult.Healthy("Database connection is healthy");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database connection failed", ex);
        }
    }
}
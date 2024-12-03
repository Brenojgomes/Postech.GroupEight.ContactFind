using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class HealthCheckSetup
    {
        internal static void AddMongoDbHealthCheck(this IHealthChecksBuilder healthChecks, IConfiguration configuration)
        {
            string? connectionString = configuration["Mongo:ConnectionString"];
            ArgumentNullException.ThrowIfNullOrEmpty(connectionString);
            healthChecks.AddMongoDb(mongodbConnectionString: connectionString, tags: ["readiness"]);
        }
    }
}
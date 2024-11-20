using Postech.GroupEight.ContactFind.Api.HealthChecks;
using Postech.GroupEight.ContactFind.Infra;
using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class HealthCheckSetup
    {
        internal static void AddMongoDbHealthCheck(this IHealthChecksBuilder healthChecks)
        {
            healthChecks.AddCheck<MongoDbHealthCheck>(nameof(MongoDbOptions));
        }
    }
}
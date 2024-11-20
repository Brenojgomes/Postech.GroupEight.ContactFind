using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Api.HealthChecks
{
    [ExcludeFromCodeCoverage]
    public class MongoDbHealthCheck(IMongoClient mongoClient) : IHealthCheck
    {
        private readonly IMongoClient _mongoClient = mongoClient;

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Tenta acessar a lista de bancos de dados para verificar a conexão
                await _mongoClient.ListDatabaseNamesAsync(cancellationToken);
                return HealthCheckResult.Healthy("MongoDB está disponível.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("MongoDB está indisponível.", ex);
            }
        }
    }
}

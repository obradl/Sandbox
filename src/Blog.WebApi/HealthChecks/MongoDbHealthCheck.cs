using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Data.MongoDb;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blog.WebApi.HealthChecks
{
    public class MongoDbHealthCheck : IHealthCheck
    {
        private readonly MongoDbSettings _settings;
        public string Name => "MongoDbHealthCheck";

        public MongoDbHealthCheck(IOptions<MongoDbSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = new MongoClient(_settings.ConnectionString);
            BsonDocument document = null;
            try
            {
                var database = client.GetDatabase(_settings.Database);
                document = await database.RunCommandAsync((Command<BsonDocument>)"{ping:1}", cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                return new HealthCheckResult(HealthCheckStatus.Unhealthy, e, "MongoDb is offline", null);
            }

            var ok = document != null;

            return ok ? new HealthCheckResult(HealthCheckStatus.Healthy, null, "MongoDb is online", null)
                : new HealthCheckResult(HealthCheckStatus.Unhealthy, null, "MongoDb is offline", null);
        }
    }
}

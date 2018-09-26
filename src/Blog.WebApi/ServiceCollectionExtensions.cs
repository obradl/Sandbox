using System.Security.Authentication;
using Hangfire;
using Hangfire.Mongo;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Blog.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services, string connectionString, string databaseName)
        {
            services.AddHangfire(config =>
            {
                var clientSettings = MongoClientSettings.FromUrl(
                    new MongoUrl(connectionString));

                clientSettings.SslSettings =
                    new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

                config.UseMongoStorage(clientSettings, databaseName);
            });

            return services;
        }
    }
}
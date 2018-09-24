using System.Security.Authentication;
using Blog.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Blog.Infrastructure.Data
{
    public class BlogContext : IBlogContext
    {
        private readonly IMongoDatabase _database;

        public BlogContext(IOptions<MongoDbSettings> settings)
        {
            var clientSettings = MongoClientSettings.FromUrl(
                new MongoUrl(settings.Value.ConnectionString));

            clientSettings.SslSettings =
                new SslSettings {EnabledSslProtocols = SslProtocols.Tls12};

            var client = new MongoClient(clientSettings);
            _database = client.GetDatabase(settings.Value.Database);

            var conventionPack = new ConventionPack {new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
        }

        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Post");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comment");
        public IMongoCollection<PostRating> PostRatings => _database.GetCollection<PostRating>("PostRating");
    }
}
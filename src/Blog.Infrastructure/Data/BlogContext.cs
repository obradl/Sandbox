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
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);

            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
        }

        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Post");

        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comment");
    }
}

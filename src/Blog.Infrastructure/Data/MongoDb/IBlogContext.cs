using Blog.Domain.Entities;
using MongoDB.Driver;

namespace Blog.Infrastructure.Data.MongoDb
{
    public interface IBlogContext
    {
        IMongoCollection<Post> Posts { get; }

        IMongoCollection<Comment> Comments { get; }
        IMongoCollection<PostRating> PostRatings { get; }
    }
}
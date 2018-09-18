
using Blog.Domain.Entities;
using MongoDB.Driver;

namespace Blog.Infrastructure.Data
{
    public interface IBlogContext
    {
        IMongoCollection<Post> Posts { get;}

        IMongoCollection<Comment> Comments { get;}
    }
}
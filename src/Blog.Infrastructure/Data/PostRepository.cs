using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Entities;
using MongoDB.Driver;

namespace Blog.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly IBlogContext _blogContext;

        public PostRepository(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public async Task<Post> Get(string id)
        {
            var post = await _blogContext.Posts
                .Find(d => d.Id == id)
                .FirstOrDefaultAsync(CancellationToken.None);

            //var postRatings = await _blogContext.PostRatings
            //    .Find(Builders<PostRating>.Filter
            //        .Where(d => d.PostId == id))
            //    .ToListAsync();
            var postRatings = await _blogContext.PostRatings.Find(d => d.PostId == id).ToListAsync();
            post.Ratings = postRatings;

            return post;
        }


        public async Task Update(Post post)
        {
            await _blogContext.Posts.ReplaceOneAsync(r => r.Id == post.Id, post);
        }

        public Task Insert(Post post)
        {
            var postCollection = _blogContext.Posts;
            return postCollection.InsertOneAsync(post);
        }

        public async Task<IEnumerable<Post>> GetAll(bool published)
        {
            return await _blogContext.Posts
                .Find(Builders<Post>.Filter.Where(d => d.Published == published)).ToListAsync();
        }

        public Task Delete(string id)
        {
            return _blogContext.Posts.DeleteOneAsync(d => d.Id == id);
        }

        public async Task AddRating(string id, int rating)
        {
            var postRating = new PostRating(id, rating);
            await _blogContext.PostRatings.InsertOneAsync(postRating);
        }
    }
}

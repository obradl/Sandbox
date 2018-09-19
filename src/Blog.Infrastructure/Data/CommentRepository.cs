using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Entities;
using MongoDB.Driver;

namespace Blog.Infrastructure.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IBlogContext _blogContext;

        public CommentRepository(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public async Task<Comment> Get(string id)
        {
            var comments = await _blogContext.Comments
                .Find(d => d.Id == id)
                .FirstOrDefaultAsync(CancellationToken.None);

            return comments;
        }

        public async Task Update(Comment comment)
        {
            await _blogContext.Comments.ReplaceOneAsync(r => r.Id == comment.Id, comment);
        }

        public Task Insert(Comment comment)
        {
            return _blogContext.Comments.InsertOneAsync(comment);
        }

        public async Task<IEnumerable<Comment>> GetAll(string postId)
        {
            return await _blogContext.Comments.Find(d=>d.PostId == postId).ToListAsync();
        }

        public async Task Delete(string id)
        {
            await _blogContext.Comments.DeleteOneAsync(d => d.Id == id);
        }
    }
}
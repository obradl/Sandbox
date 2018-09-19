using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Domain.Entities;

namespace Blog.Infrastructure.Data
{
    public interface ICommentRepository
    {
        Task<Comment> Get(string id);
        Task Update(Comment comment);
        Task Insert(Comment comment);
        Task<IEnumerable<Comment>> GetAll(string postId);
        Task Delete(string commentId);
    }
}
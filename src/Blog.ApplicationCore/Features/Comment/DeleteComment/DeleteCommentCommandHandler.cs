using System.Threading;
using System.Threading.Tasks;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Data.MongoDb;
using MediatR;

namespace Blog.ApplicationCore.Features.Comment.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly IBlogContext _blogContext;

        public DeleteCommentCommandHandler(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            await _blogContext.Comments.DeleteOneAsync(request.CommentId, cancellationToken);
            return Unit.Value;
        }
    }

    public class DeleteCommentCommand : IRequest
    {
        public DeleteCommentCommand(string commentId)
        {
            CommentId = commentId;
        }

        public string CommentId { get; }
    }
}
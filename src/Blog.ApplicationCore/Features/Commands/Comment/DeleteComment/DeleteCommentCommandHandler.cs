using System.Threading;
using System.Threading.Tasks;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Commands.Comment.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;

        public DeleteCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            await _commentRepository.Delete(request.CommentId);
            return Unit.Value;
        }
    }

    public class DeleteCommentCommand : IRequest
    {
        public string CommentId { get; set; }
    }
}

using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.ApplicationCore.Features.Comment.Commands.DeleteComment
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
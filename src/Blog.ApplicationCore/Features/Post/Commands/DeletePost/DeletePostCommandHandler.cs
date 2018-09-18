using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.Delete(request.PostId);
            return  Unit.Value;
        }
    }

    public class DeletePostCommand : IPostRequest, IRequest<Unit>
    {
    }
}

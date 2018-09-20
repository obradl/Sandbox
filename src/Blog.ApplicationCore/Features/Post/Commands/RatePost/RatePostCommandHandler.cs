using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Commands.RatePost
{
    public class RatePostCommandHandler : IRequestHandler<RatePostCommand>
    {
        private readonly IPostRepository _postRepository;

        public RatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(RatePostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.AddRating(request.PostId, request.Rating);
            return Unit.Value;
        }
    }

    public class RatePostCommand : IPostRequest, IRequest<Unit>
    {
        public int Rating { get; set; }
        public string PostId { get; set; }
    }
}
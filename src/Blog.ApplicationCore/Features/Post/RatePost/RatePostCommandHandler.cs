using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.RatePost
{
    public class RatePostCommandHandler : IRequestHandler<RatePostCommand>
    {
        private readonly IBlogContext _blogContext;

        public RatePostCommandHandler(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<Unit> Handle(RatePostCommand request, CancellationToken cancellationToken)
        {
            var postRating = new PostRating(request.PostId, request.Rating);
            await _blogContext.PostRatings.InsertOneAsync(postRating, cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }

    public class RatePostCommand : IPostRequest, IRequest<Unit>
    {
        public RatePostCommand(string postId, int rating)
        {
            PostId = postId;
            Rating = rating;
        }

        public int Rating { get; }
        public string PostId { get; }
    }
}
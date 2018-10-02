using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Features.Post.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;

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
            var existingPost = await _blogContext.Posts
                .Find(d => d.Id == request.PostId)
                .FirstOrDefaultAsync(CancellationToken.None);

            var rating = existingPost.AddRating(request.Rating);

            await _blogContext.PostRatings
                .InsertOneAsync(rating, cancellationToken: cancellationToken);

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
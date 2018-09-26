using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Infrastructure.Data;
using Hangfire;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Features.Post.PublishFuturePost
{
    public class PublishFuturePostCommandHandler : IRequestHandler<PublishFuturePostCommand>
    {
        private readonly IBlogContext _blogContext;

        public PublishFuturePostCommandHandler(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public Task<Unit> Handle(PublishFuturePostCommand request, CancellationToken cancellationToken)
        {
            var scheduleTime = request.PublishDate.ToLocalTime() - DateTime.Now.ToLocalTime();
            BackgroundJob.Schedule(() => PublishPost(request.PostId), scheduleTime);

            return Task.FromResult(Unit.Value);
        }

        public async Task PublishPost(string postId)
        {
            var existingPost = await _blogContext.Posts
                .Find(d => d.Id == postId)
                .FirstOrDefaultAsync(CancellationToken.None);

            existingPost.Publish();

            await _blogContext.Posts.ReplaceOneAsync(r => r.Id == postId, existingPost);
        }
    }

    public class PublishFuturePostCommand : IRequest<Unit>
    {
        public string PostId { get; }
        public DateTime PublishDate { get; }

        public PublishFuturePostCommand(string postId, DateTime publishDate)
        {
            PostId = postId;
            PublishDate = publishDate;
        }
    }
}

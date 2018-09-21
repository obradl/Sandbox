using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Features.Post.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IBlogContext _blogContext;

        public DeletePostCommandHandler(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            await _blogContext.Posts.DeleteOneAsync(d => d.Id == request.PostId, cancellationToken);
            return Unit.Value;
        }
    }

    public class DeletePostCommand : IPostRequest, IRequest<Unit>
    {
        public string PostId { get; set; }
    }
}
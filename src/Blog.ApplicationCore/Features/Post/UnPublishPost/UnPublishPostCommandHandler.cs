using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Features.Post.UnPublishPost
{
    public class UnPublishPostCommandHandler : IRequestHandler<UnPublishPostCommand, PostDto>
    {
        private readonly IBlogContext _blogContext;
        private readonly IMapper _mapper;

        public UnPublishPostCommandHandler(IBlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(UnPublishPostCommand request, CancellationToken cancellationToken)
        {
            var existingPost = await _blogContext.Posts
                .Find(d => d.Id == request.PostId)
                .FirstOrDefaultAsync(CancellationToken.None);

            existingPost.UnPublish();

            await _blogContext.Posts.ReplaceOneAsync(r => r.Id == request.PostId, existingPost,
                cancellationToken: cancellationToken);

            var postRatings = await _blogContext.PostRatings.Find(d => d.PostId == existingPost.Id)
                .ToListAsync(cancellationToken);
            existingPost.Ratings = postRatings;

            var postDto = _mapper.Map<Domain.Entities.Post, PostDto>(existingPost);
            return postDto;
        }
    }

    public class UnPublishPostCommand : IPostRequest, IRequest<PostDto>
    {
        public string PostId { get; }

        public UnPublishPostCommand(string postId)
        {
            PostId = postId;
        }
    }
}
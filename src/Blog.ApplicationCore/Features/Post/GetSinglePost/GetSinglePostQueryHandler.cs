using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Features.Post.GetSinglePost
{
    public class GetSinglePostQueryHandler : IRequestHandler<DeletePostCommand, PostDto>
    {
        private readonly IBlogContext _blogContext;
        private readonly IMapper _mapper;

        public GetSinglePostQueryHandler(IBlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var existingPost = await _blogContext.Posts
                .Find(d => d.Id == request.PostId)
                .FirstOrDefaultAsync(CancellationToken.None);

            var postRatings = await _blogContext.PostRatings.Find(d => d.PostId == existingPost.Id).ToListAsync();
            existingPost.Ratings = postRatings;
            var postDto = _mapper.Map<Domain.Entities.Post, PostDto>(existingPost);

            return postDto;
        }
    }

    public class DeletePostCommand : IPostRequest, IRequest<PostDto>
    {
        public string PostId { get;}

        public DeletePostCommand(string postId)
        {
            PostId = postId;
        }
    }
}
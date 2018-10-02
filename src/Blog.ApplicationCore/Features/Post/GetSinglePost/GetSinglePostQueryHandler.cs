using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Features.Post.PostUtils;
using Blog.ApplicationCore.Features.Post.Utils.Dto;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Data.MongoDb;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Features.Post.GetSinglePost
{
    public class GetSinglePostQueryHandler : IRequestHandler<GetSinglePostQuery, PostDto>
    {
        private readonly IBlogContext _blogContext;
        private readonly IMapper _mapper;

        public GetSinglePostQueryHandler(IBlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetSinglePostQuery request, CancellationToken cancellationToken)
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

    public class GetSinglePostQuery : IPostRequest, IRequest<PostDto>
    {
        public GetSinglePostQuery(string postId)
        {
            PostId = postId;
        }

        public string PostId { get; }
    }
}
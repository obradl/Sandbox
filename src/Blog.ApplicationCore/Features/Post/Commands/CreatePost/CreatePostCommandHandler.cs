using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Features.Post.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        private readonly IBlogContext _blogContext;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IBlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var incomingPostDto = request.Post;

            var post = new Domain.Entities.Post(incomingPostDto.Title,
                incomingPostDto.Author,
                incomingPostDto.Body,
                incomingPostDto.Lead);

            await _blogContext.Posts.InsertOneAsync(post, cancellationToken: cancellationToken);

            var createdPost = await _blogContext.Posts
                .Find(d => d.Id == post.Id)
                .FirstOrDefaultAsync(CancellationToken.None);

            var postRatings = await _blogContext.PostRatings.Find(d => d.PostId == createdPost.Id)
                .ToListAsync(cancellationToken);
            createdPost.Ratings = postRatings;

            var postDto = _mapper.Map<Domain.Entities.Post, PostDto>(createdPost);
            return postDto;
        }
    }

    public class CreatePostCommand : IRequest<PostDto>
    {
        public CreatePostDto Post { get; set; }
    }
}
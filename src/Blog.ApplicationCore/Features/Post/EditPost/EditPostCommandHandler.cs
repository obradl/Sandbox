using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.ApplicationCore.Features.Post.Dto;
using Blog.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Features.Post.EditPost
{
    public class EditPostCommandHandler : IRequestHandler<EditPostCommand, PostDto>
    {
        private readonly IBlogContext _blogContext;
        private readonly IMapper _mapper;

        public EditPostCommandHandler(IBlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var existingPost = await _blogContext.Posts
                .Find(d => d.Id == request.PostId)
                .FirstOrDefaultAsync(CancellationToken.None);

            existingPost.SetAuthor(request.Post.Author);
            existingPost.SetBody(request.Post.Body);
            existingPost.SetLead(request.Post.Lead);
            existingPost.SetTitle(request.Post.Title);

            await _blogContext.Posts.ReplaceOneAsync(r => r.Id == request.PostId, existingPost,
                cancellationToken: cancellationToken);

            var postRatings = await _blogContext.PostRatings.Find(d => d.PostId == existingPost.Id)
                .ToListAsync(cancellationToken);
            existingPost.Ratings = postRatings;

            var postDto = _mapper.Map<Domain.Entities.Post, PostDto>(existingPost);
            return postDto;
        }
    }

    public class EditPostCommand : IPostRequest, IRequest<PostDto>
    {
        public EditPostCommand(string postId, EditPostDto post)
        {
            PostId = postId;
            Post = post;
        }

        public EditPostDto Post { get; }
        public string PostId { get; }
    }
}
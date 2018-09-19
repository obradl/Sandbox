using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Queries.GetSinglePost
{
    public class GetSinglePostQueryHandler : IRequestHandler<GetSinglePostQuery, PostDto>
    {
        private readonly IPostRepository _postRepository;

        public GetSinglePostQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostDto> Handle(GetSinglePostQuery request, CancellationToken cancellationToken)
        {
            var existingPost = await _postRepository.Get(request.PostId);

            var postDto = new PostDto
            {
                Id = existingPost.Id,
                Lead = existingPost.Lead,
                Body = existingPost.Body,
                Title = existingPost.Title,
                Author = existingPost.Author,
                DateCreated = existingPost.DateCreated,
                Published = existingPost.Published,
                DatePublished = existingPost.DatePublished
            };

            return postDto;
        }
    }

    public class GetSinglePostQuery : IPostRequest, IRequest<PostDto>
    {
        public string PostId { get; set; }
    }
}

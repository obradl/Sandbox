using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Common.Dto;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Queries.GetPosts
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostDto>>
    {
        private readonly IPostRepository _postRepository;

        public GetPostsQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var existingPosts = await _postRepository.GetAll(published: request.Filter.PublishedOnly);
            var postsDto = new List<PostDto>();

            foreach (var post in existingPosts)
            {
                postsDto.Add(new PostDto
                {
                    Id = post.Id,
                    Lead = post.Lead,
                    Body = post.Body,
                    Title = post.Title,
                    Author = post.Author,
                    DateCreated = post.DateCreated,
                    Published = post.Published,
                    DatePublished = post.DatePublished
                });
            }

            return postsDto;
        }
    }

    public class GetPostsQuery : IRequest<List<PostDto>>
    {
        public PostsQueryFilter Filter { get; set; }
    }

    public class PostsQueryFilter
    {
        public bool PublishedOnly { get; set; }
    }
}

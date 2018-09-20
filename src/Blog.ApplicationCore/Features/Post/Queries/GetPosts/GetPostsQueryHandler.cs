using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Features.Post.Queries.GetPosts
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IEnumerable<PostDto>>
    {
        private readonly IBlogContext _blogContext;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(IBlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _blogContext.Posts
                .Find(Builders<Domain.Entities.Post>
                    .Filter.Where(d => d.Published == request.PublishedOnly))
                .ToListAsync(cancellationToken);

            var postIds = posts.Select(d => d.Id).ToList();
            var postRatings = await _blogContext.PostRatings
                .Find(d => postIds.Contains(d.PostId))
                .ToListAsync(cancellationToken);

            var ratingsByPostId = postRatings.GroupBy(d => d.PostId);

            foreach (var pr in ratingsByPostId)
            {
                var postId = pr.Key;
                var post = posts.FirstOrDefault(d => d.Id == postId);

                if (post != null) post.Ratings = pr.ToList();
            }

            var postsDto = _mapper.Map<List<Domain.Entities.Post>, List<PostDto>>(posts);

            return postsDto;
        }
    }

    public class GetPostsQuery : IRequest<IEnumerable<PostDto>>
    {
        public bool PublishedOnly { get; set; }
    }
}
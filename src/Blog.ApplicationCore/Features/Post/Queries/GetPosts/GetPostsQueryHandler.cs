using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Queries.GetPosts
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IEnumerable<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public GetPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var existingPosts = await _postRepository.GetAll(request.PublishedOnly);
            var postsDto = _mapper.Map<List<Domain.Entities.Post>, List<PostDto>>(existingPosts.ToList());

            return postsDto;
        }
    }

    public class GetPostsQuery : IRequest<IEnumerable<PostDto>>
    {
        public bool PublishedOnly { get; set; }
    }
}
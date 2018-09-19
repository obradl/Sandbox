using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Queries.GetSinglePost
{
    public class GetSinglePostQueryHandler : IRequestHandler<GetSinglePostQuery, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetSinglePostQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetSinglePostQuery request, CancellationToken cancellationToken)
        {
            var existingPost = await _postRepository.Get(request.PostId);
            var postDto = _mapper.Map<Domain.Entities.Post, PostDto>(existingPost);

            return postDto;
        }
    }

    public class GetSinglePostQuery : IPostRequest, IRequest<PostDto>
    {
        public string PostId { get; set; }
    }
}

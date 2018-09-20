using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Commands.UnPublishPost
{
    public class UnPublishPostCommandHandler : IRequestHandler<UnPublishPostCommand, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public UnPublishPostCommandHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(UnPublishPostCommand request, CancellationToken cancellationToken)
        {        
            var existingPost = await _postRepository.Get(request.PostId);
            existingPost.UnPublish();

            await _postRepository.Update(existingPost);

            var postDto =  _mapper.Map<Domain.Entities.Post, PostDto>(existingPost);
            return postDto;
        }
    }

    public class UnPublishPostCommand : IPostRequest, IRequest<PostDto>
    {
        public string PostId { get; set; }
    }
}

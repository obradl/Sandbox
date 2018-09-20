using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Commands.PublishPost
{
    public class PublishPostCommandHandler : IRequestHandler<PublishPostCommand, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PublishPostCommandHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(PublishPostCommand request, CancellationToken cancellationToken)
        {        
            var existingPost = await _postRepository.Get(request.PostId);
            existingPost.Publish();

            await _postRepository.Update(existingPost);

            var postDto =  _mapper.Map<Domain.Entities.Post, PostDto>(existingPost);
            return postDto;
        }
    }

    public class PublishPostCommand : IPostRequest, IRequest<PostDto>
    {
        public string PostId { get; set; }
    }
}

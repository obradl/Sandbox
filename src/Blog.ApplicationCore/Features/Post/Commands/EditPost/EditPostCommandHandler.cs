using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Commands.EditPost
{
    public class EditPostCommandHandler : IRequestHandler<EditPostCommand, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public EditPostCommandHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<PostDto> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var post =  await _postRepository.Get(request.PostId);
            post.Author = post.Author;
            post.Body = post.Body;
            post.Lead = post.Lead;
            post.Title = post.Title;

            await _postRepository.Update(post);

            var updatedPost = _mapper.Map<Domain.Entities.Post, PostDto>(post);
            return updatedPost;
        }
    }

    public class EditPostCommand : IPostRequest, IRequest<PostDto>
    {
        public EditPostDto Post { get; set; }
    }
}

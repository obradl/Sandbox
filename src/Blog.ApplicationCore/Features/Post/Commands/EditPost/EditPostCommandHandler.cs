using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Commands.EditPost
{
    public class EditPostCommandHandler : IRequestHandler<EditPostCommand, PostDto>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public EditPostCommandHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var incomingPost = request.Post;
            var post = await _postRepository.Get(request.PostId);

            post.SetAuthor(incomingPost.Author);
            post.SetBody(incomingPost.Body);
            post.SetLead(incomingPost.Lead);
            post.SetTtile(incomingPost.Title);
            
            await _postRepository.Update(post);

            var updatedPost = _mapper.Map<Domain.Entities.Post, PostDto>(post);
            return updatedPost;
        }
    }

    public class EditPostCommand : IPostRequest, IRequest<PostDto>
    {
        public EditPostDto Post { get; set; }
        public string PostId { get; set; }
    }
}
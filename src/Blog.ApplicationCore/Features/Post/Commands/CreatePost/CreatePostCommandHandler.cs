﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Post.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var incomingPostDto = request.Post;

            var post = new Domain.Entities.Post(incomingPostDto.Title,
                incomingPostDto.Author, incomingPostDto.Body,
                incomingPostDto.Lead);

            await _postRepository.Insert(post);
            var postDto = _mapper.Map<Domain.Entities.Post, PostDto>(post);

            return postDto;
        }
    }

    public class CreatePostCommand : IRequest<PostDto>
    {
        public CreatePostDto Post { get; set; }
    }
}

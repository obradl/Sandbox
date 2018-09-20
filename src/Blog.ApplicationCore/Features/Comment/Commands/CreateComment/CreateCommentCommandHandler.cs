﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Comment.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
    {
        private readonly IBlogContext _blogContext;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(IBlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Domain.Entities.Comment(request.Comment.Author, request.Comment.Body, request.PostId);
            await _blogContext.Comments.InsertOneAsync(comment, cancellationToken: cancellationToken);

            var createdComment = _mapper.Map<Domain.Entities.Comment, CommentDto>(comment);
            return createdComment;
        }
    }

    public class CreateCommentCommand : IRequest<CommentDto>
    {
        public CreateCommentDto Comment { get; set; }
        public string PostId { get; set; }
    }
}
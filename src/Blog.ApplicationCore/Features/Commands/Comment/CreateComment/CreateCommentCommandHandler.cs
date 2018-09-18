using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Commands.Comment.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Domain.Entities.Comment()
            {
                PostId = request.PostId,
                Author = request.Comment.Author,
                Body = request.Comment.Body,
                DateCreated = DateTime.Now
            };

            await _commentRepository.Insert(comment);

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

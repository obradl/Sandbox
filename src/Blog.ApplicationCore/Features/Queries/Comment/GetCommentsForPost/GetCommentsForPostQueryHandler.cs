using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Queries.Comment.GetCommentsForPost
{
    public class GetCommentsForPostQueryHandler : IRequestHandler<GetCommentsForPostQuery, List<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentsForPostQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentDto>> Handle(GetCommentsForPostQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetAll(request.PostId);
            var commentDtos = _mapper.Map<List<Domain.Entities.Comment>, List<CommentDto>>(comments);
            return commentDtos;
        }
    }

    public class GetCommentsForPostQuery : IPostRequest, IRequest<List<CommentDto>>
    {
    }
}

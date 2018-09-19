using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Features.Comment.Queries.GetCommentsForPost
{
    public class GetCommentsForPostQueryHandler : IRequestHandler<GetCommentsForPostQuery, IEnumerable<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentsForPostQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDto>> Handle(GetCommentsForPostQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetAll(request.PostId);
            var commentDtos = _mapper.Map<IEnumerable<Domain.Entities.Comment>, IEnumerable<CommentDto>>(comments);
            return commentDtos;
        }
    }

    public class GetCommentsForPostQuery : IPostRequest, IRequest<IEnumerable<CommentDto>>
    {
    }
}

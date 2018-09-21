using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Features.Comment.GetCommentsForPost
{
    public class GetCommentsForPostQueryHandler : IRequestHandler<GetCommentsForPostQuery, IEnumerable<CommentDto>>
    {
        private readonly IBlogContext _blogContext;
        private readonly IMapper _mapper;

        public GetCommentsForPostQueryHandler(IBlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDto>> Handle(GetCommentsForPostQuery request,
            CancellationToken cancellationToken)
        {
            var comments = await _blogContext.Comments
                .Find(d => d.PostId == request.PostId)
                .ToListAsync(CancellationToken.None);

            var commentDtos = _mapper.Map<IEnumerable<Domain.Entities.Comment>, IEnumerable<CommentDto>>(comments);
            return commentDtos;
        }
    }

    public class GetCommentsForPostQuery : IPostRequest, IRequest<IEnumerable<CommentDto>>
    {
        public string PostId { get; set; }
    }
}
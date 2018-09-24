using AutoMapper;
using Blog.ApplicationCore.Features.Comment.Utils.Dto;

namespace Blog.ApplicationCore.Features.Comment.Utils.Mapping
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Domain.Entities.Comment, CommentDto>();
        }
    }
}
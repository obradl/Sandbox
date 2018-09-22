using AutoMapper;
using Blog.ApplicationCore.Features.Comment.Dto;

namespace Blog.ApplicationCore.Features.Comment.Mapping
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Domain.Entities.Comment, CommentDto>();
        }
    }
}
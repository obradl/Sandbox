using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.Domain.Entities;

namespace Blog.ApplicationCore.Common.Mapping
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentDto>();
        }
    }
}
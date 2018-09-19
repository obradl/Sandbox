using AutoMapper;
using Blog.ApplicationCore.Common.Dto;
using Blog.Domain.Entities;

namespace Blog.ApplicationCore.Common.Mapping
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(d=>d.Rating, opt=>opt.MapFrom(d => d.CalculateAverageRating()));
        }
    }
}

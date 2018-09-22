using AutoMapper;
using Blog.ApplicationCore.Features.Post.Dto;

namespace Blog.ApplicationCore.Features.Post.Mapping
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Domain.Entities.Post, PostDto>()
                .ForMember(d => d.Rating, opt => opt.MapFrom(d => d.CalculateAverageRating()));
        }
    }
}
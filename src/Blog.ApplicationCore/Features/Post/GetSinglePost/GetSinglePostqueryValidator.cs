using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.GetSinglePost
{
    public class GetSinglePostQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetSinglePostQueryValidator()
        {
            RuleFor(d => d.PostId).NotEmpty();
        }
    }
}
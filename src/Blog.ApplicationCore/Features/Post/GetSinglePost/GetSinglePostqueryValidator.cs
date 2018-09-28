using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.GetSinglePost
{
    public class GetSinglePostQueryValidator : AbstractValidator<GetSinglePostQuery>
    {
        public GetSinglePostQueryValidator()
        {
            RuleFor(d => d.PostId).NotEmpty();
        }
    }
}
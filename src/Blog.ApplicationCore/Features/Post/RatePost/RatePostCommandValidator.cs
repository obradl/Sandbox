using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.RatePost
{
    public class RatePostCommandValidator : AbstractValidator<RatePostCommand>
    {
        public RatePostCommandValidator()
        {
            RuleFor(d => d.PostId).NotEmpty();
            RuleFor(d => d.Rating)
                .GreaterThan(0)
                .LessThan(6);
        }
    }
}
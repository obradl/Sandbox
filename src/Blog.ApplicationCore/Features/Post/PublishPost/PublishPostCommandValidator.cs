using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.PublishPost
{
    public class PublishPostCommandValidator : AbstractValidator<PublishPostCommand>
    {
        public PublishPostCommandValidator()
        {
            RuleFor(d => d.PostId).NotEmpty();
        }
    }
}
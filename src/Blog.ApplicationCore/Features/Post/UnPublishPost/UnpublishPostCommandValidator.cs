using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.UnPublishPost
{
    public class UnpublishPostCommandValidator : AbstractValidator<UnPublishPostCommand>
    {
        public UnpublishPostCommandValidator()
        {
            RuleFor(d => d.PostId).NotEmpty();
        }
    }
}
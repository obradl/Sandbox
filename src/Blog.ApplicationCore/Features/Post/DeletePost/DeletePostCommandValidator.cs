using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.DeletePost
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(d => d.PostId).NotEmpty();
        }
    }
}
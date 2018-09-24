using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.EditPost
{
    public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostCommandValidator()
        {
            RuleFor(d => d.Post).SetValidator(new EditPostValidator());
            RuleFor(d => d.PostId).NotEmpty();
        }
    }

    public class EditPostValidator : AbstractValidator<EditPostDto>
    {
        public EditPostValidator()
        {
            RuleFor(d => d.Body).NotEmpty();
            RuleFor(d => d.Author).NotEmpty();
            RuleFor(d => d.Lead).NotEmpty();
            RuleFor(d => d.Title).NotEmpty();
        }
    }
}
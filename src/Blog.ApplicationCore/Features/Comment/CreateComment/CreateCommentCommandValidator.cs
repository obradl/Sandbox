using FluentValidation;

namespace Blog.ApplicationCore.Features.Comment.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(d => d.Comment).SetValidator(new CreateCommentValidator());
            RuleFor(d => d.PostId).NotEmpty();
        }
    }

    public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentValidator()
        {
            RuleFor(d => d.Body).NotEmpty();
            RuleFor(d => d.Author).NotEmpty();
        }
    }
}
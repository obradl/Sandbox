using FluentValidation;

namespace Blog.ApplicationCore.Features.Comment.DeleteComment
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(d => d.CommentId).NotEmpty();
        }
    }
}
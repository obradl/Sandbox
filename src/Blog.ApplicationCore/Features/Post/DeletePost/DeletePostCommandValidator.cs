using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.DeletePost
{
    public class DeletePostCommandValidator : AbstractValidator<GetSinglePost.GetPostByIdQuery>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(d => d.PostId).NotEmpty();
        }
    }
}
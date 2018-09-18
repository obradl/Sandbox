using FluentValidation;

namespace Blog.ApplicationCore.Features.Commands.Post.CreatePost
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(d => d.Post.Author).NotEmpty();
        }
    }
}

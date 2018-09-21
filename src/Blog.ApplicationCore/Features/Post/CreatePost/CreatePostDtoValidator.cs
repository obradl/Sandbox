using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(d => d.Post).SetValidator(new CreatePostDtoValidator());
        }
    }

    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(d => d.Body).NotEmpty();
            RuleFor(d => d.Author).NotEmpty();
            RuleFor(d => d.Lead).NotEmpty();
            RuleFor(d => d.Title).NotEmpty();
        }
    }
}
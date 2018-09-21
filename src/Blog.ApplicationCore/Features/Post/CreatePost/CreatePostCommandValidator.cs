using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(d => d.Post).SetValidator(new CreatePostValidator());
        }
    }

    public class CreatePostValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostValidator()
        {
            RuleFor(d => d.Body).NotEmpty();
            RuleFor(d => d.Author).NotEmpty();
            RuleFor(d => d.Lead).NotEmpty();
            RuleFor(d => d.Title).NotEmpty();
        }
    }
}
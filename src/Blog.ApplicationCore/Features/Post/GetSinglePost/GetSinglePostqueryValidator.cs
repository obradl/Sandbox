using FluentValidation;

namespace Blog.ApplicationCore.Features.Post.GetSinglePost
{
    public class GetSinglePostQueryValidator : AbstractValidator<DeletePostCommand>
    {
        public GetSinglePostQueryValidator()
        {
            RuleFor(d => d.PostId).NotEmpty();
        }
    }
}
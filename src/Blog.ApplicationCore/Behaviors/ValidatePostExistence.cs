using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Features.Post.PostUtils;
using Blog.Domain.Exceptions;
using Blog.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Behaviors
{
    public class ValidatePostExistence<TRequest, TResponse> 
        : IPipelineBehavior<IPostRequest, TResponse>  
       
    { 
        private readonly IBlogContext _blogContext;

        public ValidatePostExistence(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<TResponse> Handle(IPostRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Domain.Entities.Post post;
            try
            {
                post = await _blogContext.Posts
                    .Find(d => d.Id == request.PostId)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            catch (FormatException e)
            {
                throw new EntityDoesNotExistsException($"The post with id {request.PostId} does not exist.");
            }
            if (post == null)
            {
                throw new EntityDoesNotExistsException($"The post with id {request.PostId} does not exist.");
            }

            var response = await next();
            return response;
        }
    }
}

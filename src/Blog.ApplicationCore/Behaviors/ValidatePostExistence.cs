using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Features.Post.PostUtils;
using Blog.Domain.Exceptions;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Data.MongoDb;
using MediatR;
using MongoDB.Driver;

namespace Blog.ApplicationCore.Behaviors
{
    public class ValidatePostExistence<TResponse> : IPipelineBehavior<IPostRequest, TResponse>
       
    { 
        private readonly IBlogContext _blogContext;

        public ValidatePostExistence(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<TResponse> Handle(IPostRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Domain.Entities.Post post = null;
            try
            {
                post = await _blogContext.Posts
                    .Find(d => d.Id == request.PostId)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            catch (FormatException)
            {
                ThrowEntityDoesNotExistsException(request);
            }
            if (post == null)
            {
                ThrowEntityDoesNotExistsException(request);
            }

            var response = await next();
            return response;
        }

        private static void ThrowEntityDoesNotExistsException(IPostRequest request)
        {
            throw new EntityDoesNotExistsException($"The post with id {request.PostId} does not exist.");
        }
    }
}

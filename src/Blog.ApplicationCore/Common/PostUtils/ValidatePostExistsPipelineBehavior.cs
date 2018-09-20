using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Blog.Infrastructure.Data;
using MediatR;

namespace Blog.ApplicationCore.Common.PostUtils
{
    public class ValidatePostExistsPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IPostRequest
    {
        private readonly IPostRepository _postRepository;

        public ValidatePostExistsPipelineBehavior(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            Domain.Entities.Post post = null;
            try
            {
                post = await _postRepository.Get(request.PostId);
            }
            catch (FormatException)
            {
                CreateException(request);
            }
        
            if (post == null)
            {
                CreateException(request);
            }

            return await next();
        }

        private static void CreateException(TRequest request)
        {
            throw new EntityDoesNotExistsException($"Blog post with id {request.PostId} does not exist");
        }
    }
}
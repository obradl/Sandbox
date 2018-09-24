using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Blog.ApplicationCore.Behaviors
{
    public class ValidateBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidateBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                throw new DomainValidationException($"Validation Errors for type {typeof(TRequest).Name}", 
                    failures.Select(d=>d.ErrorMessage));
            }
              
            var response = await next();
            return response;
        }
    }
}
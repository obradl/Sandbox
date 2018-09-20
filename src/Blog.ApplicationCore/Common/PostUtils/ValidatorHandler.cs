//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using FluentValidation;
//using MediatR;

//public class ValidatorHandler<TRequest, TResponse>
//    : IRequestHandler<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//{

//    private readonly IRequestHandler<TRequest, TResponse> _inner;
//    private readonly IValidator<TRequest>[] _validators;

//    public ValidatorHandler(IRequestHandler<TRequest, TResponse> inner,
//        IValidator<TRequest>[] validators)
//    {
//        _inner = inner;
//        _validators = validators;
//    }

//    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
//    {
//        var context = new ValidationContext(request);

//        var failures = _validators
//            .Select(v => v.Validate(context))
//            .SelectMany(result => result.Errors)
//            .Where(f => f != null)
//            .ToIEnumerable();

//        if (failures.Any())
//            throw new ValidationException(failures);

//        return _inner.Handle(request, cancellationToken);
//    }
//}


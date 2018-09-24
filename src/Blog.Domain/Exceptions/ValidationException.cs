using System.Collections.Generic;

namespace Blog.Domain.Exceptions
{
    public class ValidationException : DomainException
    {
        public IEnumerable<string> Errors { get; }
        public ValidationException(string s, IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
using System.Collections.Generic;

namespace Blog.Domain.Exceptions
{
    public class DomainValidationException : DomainException
    {
        public IEnumerable<string> Errors { get; }
        public DomainValidationException(string s, IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
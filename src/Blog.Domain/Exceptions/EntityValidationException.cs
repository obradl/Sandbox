using System.Collections.Generic;

namespace Blog.Domain.Exceptions
{
    public class EntityValidationException : DomainException
    {
        public IEnumerable<string> Errors { get; }
        public EntityValidationException(string message, IEnumerable<string> errors) :base(message)
        {
            Errors = errors;
        }
    }
}
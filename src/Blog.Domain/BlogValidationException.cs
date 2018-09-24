using System.Collections.Generic;

namespace Blog.Domain
{
    public class BlogValidationException : BlogDomainException
    {
        public IEnumerable<string> Errors { get; }
        public BlogValidationException(string s, IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
using System;

namespace Blog.Domain.Exceptions
{
    public class EntityDoesNotExistsException : Exception
    {
        public EntityDoesNotExistsException(string s) : base(s)
        {
        }
    }
}
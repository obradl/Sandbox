using System;

namespace Blog.ApplicationCore.Common
{
    public class EntityDoesNotExistsException : Exception
    {
        public EntityDoesNotExistsException(string s) : base(s)
        {
        }
    }
}

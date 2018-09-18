using System;

public class EntityDoesNotExistsException : Exception
{
    public EntityDoesNotExistsException(string s):base(s)
    {
    }
}
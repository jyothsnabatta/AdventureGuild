using System;

namespace AdventureGuild.Exceptions
{
    public class InsufficientManaException : Exception
    {
        public InsufficientManaException(string message) : base(message) 
        { 
        }
    }
}

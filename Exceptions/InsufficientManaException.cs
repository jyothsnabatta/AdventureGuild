using System;

namespace AdventureGuild.Exceptions
{

    // Custom exception used when a wizard does not have enough
    // mana to perform a spell.
    // Inherits from Exception so it can be handled as a standard exception.
    public class InsufficientManaException : Exception
    {

        // Passes the error message to the base Exception class.
        public InsufficientManaException(string message) : base(message) 
        { 
        }
    }
}

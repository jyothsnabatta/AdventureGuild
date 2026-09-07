using System;

namespace AdventureGuild.Exceptions
{

    // Custom exception used when an action is attempted on
    // a character that has already been defeated.
    // Inherits from Exception so it can be handled like other exceptions.
    public class CharacterIsDefeatedException : Exception
    {
        // Passes the error message to the base Exception class.
        public CharacterIsDefeatedException(string message) : base(message) 
        {
        }
    }
}

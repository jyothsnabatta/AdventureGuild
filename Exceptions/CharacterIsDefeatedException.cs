using System;

namespace AdventureGuild.Exceptions
{
    public class CharacterIsDefeatedException : Exception
    {
        public CharacterIsDefeatedException(string message) : base(message) 
        {
        }
    }
}

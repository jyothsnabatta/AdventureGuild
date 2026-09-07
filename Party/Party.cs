using System;
using System.Collections.Generic;
using System.Linq;
using AdventureGuild.Characters;

namespace AdventureGuild.Party
{
    // Represents a group of characters that can participate in the game.
    public class Party
    {
        // Private list keeps the party members encapsulated.
        private List<Character> characters;
        public Party()// Creates an empty party.
        {
            characters = new List<Character>();
        }

        // Adds a character to the party.
        public void AddCharacter(Character character)
        {
            // A null character cannot be added to the party.
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character), "Character cannot be null.");
            }
            characters.Add(character);
        }

        // Removes a character from the party.
        public void RemoveCharacter(Character character)
        {
            // A null character cannot be removed from the party.
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character), "Character cannot be null.");
            }
            characters.Remove(character);
        }

        // Returns only characters that are still able to participate.
        // LINQ is used to filter out defeated characters.
        public List<Character> GetMembers()
        {
            return characters.Where(character => !character.IsDefeated).ToList();
        }


        // Provides access to the party members while returning a copy
        // to prevent external code from directly modifying the original list.
        public List<Character> Characters
        { 
            get 
            {
                return new List<Character>(characters); 
            }   
        }
    }
}


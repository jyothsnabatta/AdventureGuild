using System;
using System.Collections.Generic;
using System.Linq;
using AdventureGuild.Characters;

namespace AdventureGuild.Party
{
    public class Party
    {
        private List<Character> characters;
        public Party()
        {
            characters = new List<Character>();
        }
        public void AddCharacter(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character), "Character cannot be null.");
            }
            characters.Add(character);
        }
        public void RemoveCharacter(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character), "Character cannot be null.");
            }
            characters.Remove(character);
        }
        public List<Character> GetMembers()
        {
            return characters.Where(character => !character.IsDefeated).ToList();
        }

        public List<Character> Characters
        { 
            get 
            {
                return new List<Character>(characters); 
            }   
        }
    }
}


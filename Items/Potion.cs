using System;
using AdventureGuild.Characters;

namespace AdventureGuild.Items
{ 
    // Potion is a type of Item. 
    // It can be used to heal a character.
    public class Potion : Item
    {
        // Stores how much health the potion can restore.
        private int healingAmount;

        // Constructor creates a potion with a name, value and healing amount.
        public Potion(string name, decimal value, int healingAmount) : base(name,value)
        {
            // Healing amount must be greater than zero.
            if (healingAmount <= 0)
            {
                throw new ArgumentException("Healing amount must be greater than zero.");
            }
            this.healingAmount = healingAmount;
        }

        // Allows other classes to read the healing amount. 
        // The value cannot be changed directly from outside the class.
        public int HealingAmount
        {
            get { return healingAmount; }
        }

        // Uses the potion on a character and restores health.
        public void Use(Character character)
        {
            // The character cannot be null.
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character), "Character cannot be null.");
            }
            Console.WriteLine($"{character.Name} uses {Name} and heals for {healingAmount} health points!");
            character.Heal(healingAmount);
        }
    }
}

using System;
using AdventureGuild.Characters;

namespace AdventureGuild.Items
{
    public class Potion : Item
    {
        private int healingAmount;
        public Potion(string name, decimal value, int healingAmount) : base(name,value)
        {
            if (healingAmount <= 0)
            {
                throw new ArgumentException("Healing amount must be greater than zero.");
            }
            this.healingAmount = healingAmount;
        }

        public int HealingAmount
        {
            get { return healingAmount; }
        }

        public void Use(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character), "Character cannot be null.");
            }
            Console.WriteLine($"{character.Name} uses {Name} and heals for {healingAmount} health points!");
            character.Heal(healingAmount);
        }
    }
}

using System;
using AdventureGuild.Interfaces;

namespace AdventureGuild.Dice
{
    public class RandomDiceRoller : IDiceRoller
    {
        private readonly Random random;

        public RandomDiceRoller()
        {
            random = new Random();
        }

        public int Roll(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min cannot be greater than max.");
            }
            return random.Next(min, max + 1);
        }
    }
}

using System;
using AdventureGuild.Interfaces;

namespace AdventureGuild.Dice
{
    // RandomDiceRoller is responsible for generating random numbers. 
    // It implements the IDiceRoller interface.
    public class RandomDiceRoller : IDiceRoller
    {
        // Stores the Random object used to generate numbers.
        private readonly Random random;
        // Constructor creates a new Random object.
        public RandomDiceRoller()
        {
            random = new Random();
        }
        // Generates a random number between min and max.
        public int Roll(int min, int max)
        {
            // Check that the minimum is not greater than the maximum.
            if (min > max)
            {
                throw new ArgumentException("Min cannot be greater than max.");
            }
            // Random.Next does not include the maximum, 
            // so we add 1 to include max.
            return random.Next(min, max + 1);
        }
    }
}

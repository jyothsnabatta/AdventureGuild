using System;
using AdventureGuild.Interfaces;


namespace AdventureGuild.Characters
{
    // Rogue is a specialized Character that uses critical hits
    // to deal increased damage.
    public class Rogue : Character
    {
        // Private fields keep the rogue's combat values encapsulated.
        private int attackDamage;
        private double criticalChance;
        private IDiceRoller diceRoller;

        // Constructor initializes the rogue and validates its combat values.
        // IDiceRoller can be provided to control how dice rolls are performed.
        public Rogue(string name,int level,int maxHealth,int attackDamage = 10,double criticalChance = 0.25,IDiceRoller diceRoller = null) : base(name, maxHealth, level)
        {
            // Attack damage cannot be negative.
            if (attackDamage < 0)
            {
                throw new ArgumentException("Attack damage cannot be negative.");
            }

            // Critical chance must be between 0 and 1.
            // For example, 0.25 represents a 25% critical hit chance.
            if (criticalChance < 0 || criticalChance > 1)
            {
                throw new ArgumentException("Critical chance must be between 0 and 1.");
            }

            this.attackDamage = attackDamage;
            this.criticalChance = criticalChance;

            // Uses the provided dice roller or creates a default random dice roller.
            this.diceRoller = diceRoller ?? new AdventureGuild.Dice.RandomDiceRoller(); // Use RandomDiceRoller if no dice roller was provided
        }

        // Overrides the abstract Attack method from Character.
        // The rogue has a chance to perform a critical hit.
        public override void Attack(IDamageable target)
        {
            // A valid target is required before attacking.
            if ( target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            int damage = attackDamage;

            // Roll a value between 1 and 100 to determine
            // whether the attack becomes a critical hit
            int roll = diceRoller.Roll(1, 100); // Roll a number between 1 and 100


            // A successful critical roll doubles the attack damage.
            if (roll <= criticalChance * 100)
            {
                damage *= 2; // Critical hit doubles the damage
                Console.WriteLine($"{Name} gets a critical hit!");
            }

            Console.WriteLine($"{Name} attacks quickly with a dagger!");
            Console.WriteLine($"Damage: {damage}");

            target.TakeDamage(damage);
        }
    }
}

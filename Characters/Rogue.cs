using System;
using AdventureGuild.Interfaces;


namespace AdventureGuild.Characters
{
    public class Rogue : Character
    {
        private int attackDamage;
        private double criticalChance;
        private IDiceRoller diceRoller;
        public Rogue(string name,int level,int maxHealth,int attackDamage = 10,double criticalChance = 0.25,IDiceRoller diceRoller = null) : base(name, maxHealth, level)
        {
            if(attackDamage < 0)
            {
                throw new ArgumentException("Attack damage cannot be negative.");
            }
            if(criticalChance < 0 || criticalChance > 1)
            {
                throw new ArgumentException("Critical chance must be between 0 and 1.");
            }

            this.attackDamage = attackDamage;
            this.criticalChance = criticalChance;
            this.diceRoller = diceRoller ?? new AdventureGuild.Dice.RandomDiceRoller(); // Use RandomDiceRoller if no dice roller was provided
        } 
        public override void Attack(IDamageable target)
        {
            if ( target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            int damage = attackDamage;

            // Roll between 1 and 100
            int roll = diceRoller.Roll(1, 100); // Roll a number between 1 and 100

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

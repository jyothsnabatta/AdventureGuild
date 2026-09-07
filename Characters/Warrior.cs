using System;
using AdventureGuild.Interfaces;                                            

namespace AdventureGuild.Characters
{
    // Warrior is a specialized Character with weapon damage and armor.
    // It inherits common character functionality from Character.
    public class Warrior : Character
    {
        // Private fields protect the warrior's combat values
        // from being changed directly by other classes.
        private int weaponDamage;
        private int armor;

        // Constructor initializes the warrior and validates its combat values.
        // Default values are provided for weapon damage and armor.
        public Warrior(string name, int level,int maxHealth, int weaponDamage = 15, int armor = 5) : base(name, maxHealth, level)
        {
            if(weaponDamage < 0)
            {
                throw new ArgumentException("Weapon damage cannot be negative.");
            }
            if(armor < 0)
            {
                throw new ArgumentException("Armor cannot be negative.");
            }

            this.weaponDamage = weaponDamage;
            this.armor = armor;
        }

        // Overrides the abstract Attack method from Character.
        // Warriors use their weapon damage when attacking.
        public override void Attack(IDamageable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "Target cannot be null.");
            }

            int damage = weaponDamage;

           
            Console.WriteLine($"{Name} attacks with a sword!");
            Console.WriteLine($"Damage: {damage}");


            // IDamageable allows the warrior to attack any object
            // that can receive damage without depending on a specific class.
            target.TakeDamage(damage);
        }

        // Overrides Character.TakeDamage to add warrior-specific armor.
        // Armor reduces the incoming damage before the base method is called.
        public override void TakeDamage(int amount)
            {
            int reducedDamage = amount - armor;

            // Armor cannot reduce damage below zero.
            if (reducedDamage  < 0)
            {
                    reducedDamage = 0;
            }
            
            Console.WriteLine($"{Name}'s armor reduces damage from" + $" {amount} to {reducedDamage}");


            // Reuses the common health-handling logic from Character.
            base.TakeDamage(reducedDamage);
        }
    }
}

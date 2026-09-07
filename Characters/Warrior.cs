using System;
using AdventureGuild.Interfaces;                                            

namespace AdventureGuild.Characters
{
    public class Warrior : Character
    {
        private int weaponDamage;
        private int armor;
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
        public override void Attack(IDamageable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "Target cannot be null.");
            }

            int damage = weaponDamage;

           
            Console.WriteLine($"{Name} attacks with a sword!");
            Console.WriteLine($"Damage: {damage}");

            target.TakeDamage(damage);
        }

        // Warrior has its own damage reduction,
        public override void TakeDamage(int amount)
            {
            int reducedDamage = amount - armor;
            if (reducedDamage  < 0)
            {
                    reducedDamage = 0;
            }
            
            Console.WriteLine($"{Name}'s armor reduces damage from" + $" {amount} to {reducedDamage}");

            base.TakeDamage(reducedDamage);
        }
    }
}

using AdventureGuild.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGuild.Exceptions;

namespace AdventureGuild.Characters
{
    public class Wizard : Character, ISpellcaster
    {
        private int mana;
        private int maxMana;
        public Wizard(string name,int level,int maxHealth,int maxMana = 50) : base(name, maxHealth,level)
        {
            if(maxMana <= 0)
            {
                throw new ArgumentException("Max mana must be greater than zero.");
            }
            this.maxMana = maxMana;
            this.mana = maxMana;
        } 
        public int Mana
        {
            get { return mana; }
        }

        public int MaxMana
        {
            get { return maxMana; }
        }
        public override void Attack(IDamageable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "Target cannot be null.");
            }

            int damage = 8;

            Console.WriteLine($"{Name} attacks with a staff for {damage} damage!");

            target.TakeDamage(damage);
        }

        public void CastSpell(IDamageable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "Target cannot be null.");
            }
            int spellCost = 10;
            int spellDamage = 25;

            if (mana < spellCost)
            {
                throw new InsufficientManaException($"{Name} does not have enough mana to cast a spell.");
            }

            mana -= spellCost;

            Console.WriteLine($"{Name} casts fireball for {spellDamage} damage!");

            target.TakeDamage(spellDamage);

            Console.WriteLine($"{Name} has {mana}/{maxMana} mana remaining.");

        }

        // Overloaded method
        // Allows the wizard to cast a spell with custom damage.

        public void CastSpell(IDamageable target, int damage)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "Target cannot be null.");
            }
            if (damage <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), "Custom damage must be greater than zero.");
            }

            int spellCost = 15;

            if (mana < spellCost)
            {
                throw new InsufficientManaException($"{Name} does not have enough mana to cast a spell.");
            }
            mana -= spellCost;

            Console.WriteLine($"{Name} casts a powerful spell for {damage} damage!");

            target.TakeDamage(damage);
        }

        public void RestoreMana(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Mana amount cannot be negative.");
            }
            mana += amount;

            if (mana > maxMana)
            {
                mana = maxMana; // Prevent mana from exceeding the maximum
            }
            
        }
    }
} 




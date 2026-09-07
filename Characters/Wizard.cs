using AdventureGuild.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGuild.Exceptions;

namespace AdventureGuild.Characters
{
    // Wizard is a specialized Character that can also cast spells.
    // It inherits common character functionality and implements ISpellcaster.
    public class Wizard : Character, ISpellcaster
    {
        // Private fields provide encapsulation of the wizard's mana values.
        private int mana;
        private int maxMana;

        // Constructor initializes the wizard and sets its starting mana.
        public Wizard(string name,int level,int maxHealth,int maxMana = 50) : base(name, maxHealth,level)
        {

            // Prevents the wizard from being created with an invalid maximum mana value.
            if (maxMana <= 0)
            {
                throw new ArgumentException("Max mana must be greater than zero.");
            }
            this.maxMana = maxMana;
            this.mana = maxMana;
        }

        // Read-only property that allows other classes to see
        // the current mana without being able to change it directly.
        public int Mana
        {
            get { return mana; }
        }

        // Read-only property that exposes the wizard's maximum mana.
        public int MaxMana
        {
            get { return maxMana; }
        }

        // Overrides Character.Attack with the wizard's own basic attack.
        public override void Attack(IDamageable target)
        {

            // A valid target is required before performing the attack.
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "Target cannot be null.");
            }

            int damage = 8;

            Console.WriteLine($"{Name} attacks with a staff!");
            Console.WriteLine($"Damage: {damage}");


            // IDamageable allows the wizard to attack different types of targets
            // without depending on a specific target class.
            target.TakeDamage(damage);
        }

        // Casts the wizard's standard Fireball spell.
        // The spell consumes mana and deals a fixed amount of damage.
        public void CastSpell(IDamageable target)
        {

            // Prevents the spell from being cast without a valid target.
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "Target cannot be null.");
            }
            int spellCost = 10;
            int spellDamage = 25;


            // A custom exception is used when the wizard does not have
            // enough mana to cast the spell.

            if (mana < spellCost)
            {
                throw new InsufficientManaException($"{Name} does not have enough mana to cast a spell.");
            }

            mana -= spellCost;

            Console.WriteLine($"{Name} casts Fireball!");
            Console.WriteLine($"Damage: {spellDamage}");

            target.TakeDamage(spellDamage);

            Console.WriteLine($"{Name} has {mana}/{maxMana} mana remaining.");

        }

        // Overloaded method that allows the wizard to cast a spell
        // with a custom damage value.

        public void CastSpell(IDamageable target, int damage)
        {

            // Prevents the spell from being cast without a valid target.
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "Target cannot be null.");
            }

            // Custom spell damage must be a positive value.
            if (damage <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), "Custom damage must be greater than zero.");
            }

            int spellCost = 15;


            // The custom spell also requires sufficient mana
            if (mana < spellCost)
            {
                throw new InsufficientManaException($"{Name} does not have enough mana to cast a spell.");
            }
            mana -= spellCost;

            Console.WriteLine($"{Name} casts a powerful spell for {damage} damage!");

            target.TakeDamage(damage);
        }


        // Restores mana to the wizard.
        // Mana cannot exceed the maximum mana value.
        public void RestoreMana(int amount)
        {
            // Prevents invalid negative mana restoration.
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Mana amount cannot be negative.");
            }
            mana += amount;

            if (mana > maxMana)// Prevents mana from exceeding the wizard's maximum.
            {
                mana = maxMana; // Prevent mana from exceeding the maximum
            }
            
        }
    }
} 




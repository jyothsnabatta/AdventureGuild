using AdventureGuild.Interfaces;
using AdventureGuild.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGuild.Monsters
{
    public class Monster : IDamageable
    {
        private string name;
        private int maxHealth;
        private int currentHealth;  
        private int attackPower;

        public string Name
        {
            get { return name; }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
        }
        public int CurrentHealth    
        {
            get { return currentHealth; }
        }
        public int AttackPower
        {
            get { return attackPower; }
        }
        public Monster(string name, int maxHealth, int attackPower)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }
            if (maxHealth <= 0)
            {
                throw new ArgumentException("Max health must be greater than zero.");
            }
            if (attackPower < 0)
            {
                throw new ArgumentException("Attack power cannot be negative.");
            }
            this.name = name;
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth; // Initialize current health to max health
            this.attackPower = attackPower;
        }
        public bool IsDefeated
        {
            get { return currentHealth <= 0; }
        }

        // Implements IDamageable
        public void TakeDamage(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Damage cannot be negative.");
            }
            currentHealth -= amount;
            if (currentHealth < 0)
            {
                currentHealth = 0; // Ensure health doesn't go below zero
            }

            Console.WriteLine($"{name} takes {amount} damage and has {currentHealth}/{maxHealth} health remaining.");
        }

        public void Attack(Character target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            Console.WriteLine($"{name} attacks {target.Name} for {attackPower} damage!");
            
            target.TakeDamage(attackPower);
        }
    }
}

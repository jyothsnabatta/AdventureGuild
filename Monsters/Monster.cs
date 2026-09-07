using AdventureGuild.Interfaces;
using AdventureGuild.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGuild.Monsters
{
    // Monster represents an enemy in the game. 
    // It implements IDamageable so it can receive damage.
    public class Monster : IDamageable
    {
        // Private fields protect the monster's data.
        private string name;
        private int maxHealth;
        private int currentHealth;  
        private int attackPower;

        // Public properties allow other classes to read the monster's information.
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
        // Constructor creates a monster with its name, health and attack power.
        public Monster(string name, int maxHealth, int attackPower)
        {
            // Check that the monster has a valid name.
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }
            // Max health must be greater than zero.
            if (maxHealth <= 0)
            {
                throw new ArgumentException("Max health must be greater than zero.");
            }
            // Attack power cannot be negative.
            if (attackPower < 0)
            {
                throw new ArgumentException("Attack power cannot be negative.");
            }
            this.name = name;
            this.maxHealth = maxHealth;
            // The monster starts with full health.
            this.currentHealth = maxHealth; 
            this.attackPower = attackPower;
        }

        // Returns true when the monster has no health left.
        public bool IsDefeated
        {
            get { return currentHealth <= 0; }
        }

        // Implements IDamageable
        // This method reduces the monster's health when it takes damage.
        public void TakeDamage(int amount)
        {
            // Damage cannot be negative.
            if (amount < 0)
            {
                throw new ArgumentException("Damage cannot be negative.");
            }
            currentHealth -= amount;
            // Health cannot go below zero.
            if (currentHealth < 0)
            {
                currentHealth = 0; // Ensure health doesn't go below zero
            }
            // Display the damage and remaining health.
            Console.WriteLine($"{name} takes {amount} damage.");
            Console.WriteLine($"Health: {currentHealth}/{maxHealth}");
        }

        // Allows the monster to attack a character.
        public void Attack(Character target)
        {
            // The target cannot be null.
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            Console.WriteLine($"{name} attacks!");
            Console.WriteLine($"Damage: {attackPower}");

            // Send the monster's attack damage to the target character.
            target.TakeDamage(attackPower);
        }
    }
}

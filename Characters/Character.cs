using System;
using System.Collections.Generic;
using AdventureGuild.Items;
using AdventureGuild.Interfaces;

namespace AdventureGuild.Characters
{
    public abstract class Character : IDamageable
    {
        // Properties with private setters to ensure encapsulation
        // Other classes can read them but cannot modify them directly
        public string Name { get;  private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get;  private set; }
        public int Level { get; private set; }

        // Each character owns an inventory of items.
        public List<Item> Inventory { get; private set; }

        private Dictionary<string, Item> equippedItems;

        // Protected Constructor
        // only charcater and derived classes can call this constructor.
        protected Character(string name, int maxHealth, int level)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
            }
            if (maxHealth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxHealth), "Max health must be greater than zero.");
            }
            if (level <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(level), "Level must be at least 1.", nameof(level));
            }

            Name = name;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth; // Start with full health
            Level = level;

            Inventory = new List<Item>();
        }
        // Check if character is defeated
        // returns true if CurrentHealth is 0 or less, otherwise false
        public bool IsDefeated
        {
            get 
            
            { 
                return CurrentHealth <= 0; 
            }
        }

        // Add an item to inventory
        public void AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }
            Inventory.Add(item);
          }

        // Remove an item from inventory
        public void RemoveItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }
            Inventory.Remove(item);
        }

        // Returns a copy of the inventory list to prevent external modification
        public List<Item> GetInventory()
        {
            return new List<Item>(Inventory);
        }

        public void EquipItem(string slot, Item item)
        {
            if (item == null) 
            {
                throw new ArgumentNullException(nameof(item));
            }

            equippedItems[slot] = item;
        }
        public Item GetEqippedItem(string slot)
        {
            if (equippedItems.ContainsKey(slot))
            { 
            return equippedItems[slot];
            }
            return null;
        }

        // Take damage
        public virtual void TakeDamage(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Damage amount cannot be negative.",nameof(amount));
            }

            CurrentHealth -= amount;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0; // Prevent negative health
            }
            Console.WriteLine($"{Name} takes {amount} damage. Health: {CurrentHealth}/{MaxHealth}");
        }

        // Heal the character
        public virtual void Heal(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Heal amount cannot be negative.");
            }
            CurrentHealth += amount;
            // Health cannot exceed MaxHealth
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth; // Prevent overhealing
            }
            Console.WriteLine($"{Name} heals {amount} health. Health: {CurrentHealth}/{MaxHealth}");
        }
        // Every character must have its own attack
        public abstract void Attack(IDamageable target);
    }
}








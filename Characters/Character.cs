using System;
using System.Collections.Generic;
using AdventureGuild.Items;
using AdventureGuild.Interfaces;

namespace AdventureGuild.Characters
{
    public abstract class Character : IDamageable  // Implements IDamageable so every character can take damage.
    {
        // public Properties with private setters to ensure encapsulation
        // Other classes can read them but cannot modify them directly
        // can change them, which helps maintain encapsulation.
        public string Name { get;  private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get;  private set; }
        public int Level { get; private set; }

        // Stores the items currently owned by the character.
        public List<Item> Inventory { get; private set; }

        // Stores equipped items by their equipment slot.
        private Dictionary<string, Item> equippedItems;

        // Protected Constructor because character is abstract.
        // only charcater and derived classes can call this constructor.
        protected Character(string name, int maxHealth, int level)
        {
            //Validating the character's basic information before creation
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
        // Determines whether the character has been defeated.
        // A character is defeated when its health reaches zero.
        public bool IsDefeated
        {
            get 
            
            { 
                return CurrentHealth <= 0; 
            }
        }

        // Adds an item to the character's inventory.
        public void AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }
            Inventory.Add(item);
          }

        // Removes an item from the character's inventory.
        public void RemoveItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }
            Inventory.Remove(item);
        }

        // Returns a copy of the inventory so external code
        // cannot directly modify the original list.
        public List<Item> GetInventory()
        {
            return new List<Item>(Inventory);
        }

        // Equips an item in the specified equipment slot.
        public void EquipItem(string slot, Item item)
        {
            if (item == null) 
            {
                throw new ArgumentNullException(nameof(item));
            }

            equippedItems[slot] = item;
        }

        // Returns the item equipped in the specified slot.
        // Returns null if the slot is empty.
        public Item GetEqippedItem(string slot)
        {
            if (equippedItems.ContainsKey(slot))
            { 
            return equippedItems[slot];
            }
            return null;
        }

        // Reduces the character's health by the specified damage amount.
        // Health is prevented from falling below zero.
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

        // Restores the character's health.
        // Health cannot exceed the character's maximum health.
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
        // Each character type must provide its own attack implementation.
        // This is abstract because different character classes attack differently.
        public abstract void Attack(IDamageable target);
    }
}








using System;

namespace AdventureGuild.Items
{
    // Abstract base class for all items in the game
    public abstract class Item
    {
        public string Name { get; private set; }
        public decimal Value { get; private set; } // Value of the item in gold coins
        protected Item(string name, decimal value)
        {
            Name = name;
            Value = value;
        }
        public override string ToString() 
        {
            return $"{Name} (Value: {Value:C} gold)";
        }
    }
}
 




using System;

namespace AdventureGuild.Items
{
    // Item is the base class for all items in the game. 
    // It is abstract because we do not create a general Item directly.
    public abstract class Item
    {
        // Name and Value are private-set properties. 
        // Other classes can read them, but cannot change them directly
        public string Name { get; private set; }
        public decimal Value { get; private set; }

        // Constructor sets the name and value of the item.
        protected Item(string name, decimal value)
        {
            Name = name;
            Value = value;
        }
        // Returns information about the item as text.
        public override string ToString() 
        {
            return $"{Name} (Value: {Value:C} gold)";
        }
    }
}
 




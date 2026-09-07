using System;


namespace AdventureGuild.Items
{
    // Armor is a type of Item. 
    // It inherits the Name and Value properties from Item.
    public class Armor : Item
    {
        // Stores how much defense the armor provides.
        private int defense;
        // Constructor creates armor with a name, value and defense.
        public Armor(string name, decimal value, int defense) : base(name,value)
        {
            // Defense cannot be negative.
            if (defense < 0)
            {
                throw new ArgumentException("Defense value cannot be negative.");
            }
            this.defense = defense;
        }
        // Allows other classes to read the armor's defense. 
        // The defense cannot be changed directly from outside the class.
        public int Defense
        {
            get { return defense; }
        }
    }
}

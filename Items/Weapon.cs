using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGuild.Items
{
    // Weapon is a type of Item. 
    // It inherits the Name and Value properties from Item.
    public class Weapon : Item
    {
        // Stores the amount of damage the weapon can do.
        private int damage;

        // Constructor creates a weapon with a name, value and damage.
        public Weapon(string name,decimal value, int damage) : base(name, value)
        {
            // Damage cannot be negative.
            if (damage < 0)
            {
                throw new ArgumentException("Damage value cannot be negative.");
            }
            this.damage = damage;
        }
        // Allows other classes to read the weapon's damage. 
        // The damage cannot be changed directly from outside the class.
        public int Damage
        {
            get { return damage; }
        }
    }
}

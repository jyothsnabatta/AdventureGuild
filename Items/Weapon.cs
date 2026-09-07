using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGuild.Items
{
    public class Weapon : Item
    {
        private int damage;

        public Weapon(string name,decimal value, int damage) : base(name, value)
        {
           if (damage < 0)
            {
                throw new ArgumentException("Damage value cannot be negative.");
            }
            this.damage = damage;
        }

        public int Damage
        {
            get { return damage; }
        }
    }
}

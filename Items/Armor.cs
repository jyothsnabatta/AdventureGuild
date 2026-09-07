using System;


namespace AdventureGuild.Items
{
    public class Armor : Item
    {
        private int defense;
        public Armor(string name, decimal value, int defense) : base(name,value)
        {
            if (defense < 0)
            {
                throw new ArgumentException("Defense value cannot be negative.");
            }
            this.defense = defense;
        }

        public int Defense
        {
            get { return defense; }
        }
    }
}

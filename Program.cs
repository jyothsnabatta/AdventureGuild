using AdventureGuild.Characters;
using AdventureGuild.Dice;
using AdventureGuild.Monsters;
using System;

namespace AdventureGuild
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create characters
            Warrior warrior = new Warrior("Aragorn", 5, 100);
            Wizard wizard = new Wizard("Gandalf", 5, 80);
            Rogue rogue = new Rogue("Robin", 5, 90);

            // Create party
            AdventureGuild.Party.Party party = new AdventureGuild.Party.Party();

            party.AddCharacter(warrior);
            party.AddCharacter(wizard);
            party.AddCharacter(rogue);

            // Create monster
            Monster monster = new Monster("Goblin King", 150, 12);

            // Create dice roller
            RandomDiceRoller diceRoller = new RandomDiceRoller();

            // Create battle
            AdventureGuild.Battle.Battle battle = new AdventureGuild.Battle.Battle(party, monster, diceRoller);

            // Start battle
            battle.Start();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
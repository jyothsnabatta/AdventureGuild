using AdventureGuild.Characters;
using AdventureGuild.Dice;
using AdventureGuild.Exceptions;
using AdventureGuild.Interfaces;
using AdventureGuild.Items;
using AdventureGuild.Monsters;
using System;

namespace AdventureGuild
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RandomDiceRoller diceRoller = new RandomDiceRoller();

            AdventureGuild.Party.Party party =
                new AdventureGuild.Party.Party();

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("======================================================");
                Console.WriteLine("                  THE GOLDEN DICE");
                Console.WriteLine("                 ADVENTURE GUILD");
                Console.WriteLine("======================================================");
                Console.WriteLine();

                Console.WriteLine("1. Create Characters");
                Console.WriteLine("2. View Party");
                Console.WriteLine("3. View Inventory");
                Console.WriteLine("4. Start Battle");
                Console.WriteLine("5. Game Information");
                Console.WriteLine("6. Exit");
                Console.WriteLine();

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateCharacters(party);
                        break;

                    case "2":
                        ViewParty(party);
                        break;

                    case "3":
                        ViewInventory(party);
                        break;

                    case "4":
                        StartBattle(party, diceRoller);
                        break;

                    case "5":
                        ShowGameInformation();
                        break;

                    case "6":
                        running = false;
                        Console.WriteLine();
                        Console.WriteLine("Thank you for playing!");
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        static void CreateCharacters(
            AdventureGuild.Party.Party party)
        {
            Console.Clear();

            Console.WriteLine("===========================");
            Console.WriteLine("     CREATE CHARACTERS");
            Console.WriteLine("===========================");
            Console.WriteLine();

            if (party.Characters.Count > 0)
            {
                Console.WriteLine("Characters have already been created.");
                Pause();
                return;
            }

            // Create characters
            Warrior warrior =
                new Warrior("Aragorn", 5, 100);

            Wizard wizard =
                new Wizard("Gandalf", 5, 80);

            Rogue rogue =
                new Rogue("Robin", 5, 90);

            // Create items
            Weapon sword =
                new Weapon("Sword", 50, 15);

            Armor shield =
                new Armor("Shield", 40, 5);

            Weapon magicStaff =
                new Weapon("Magic Staff", 60, 10);

            Potion healthPotion =
                new Potion("Health Potion", 20, 30);

            Weapon dagger =
                new Weapon("Dagger", 35, 12);

            // Add items to inventory
            warrior.AddItem(sword);
            warrior.AddItem(shield);

            wizard.AddItem(magicStaff);
            wizard.AddItem(healthPotion);

            rogue.AddItem(dagger);

            // Add characters to party
            party.AddCharacter(warrior);
            party.AddCharacter(wizard);
            party.AddCharacter(rogue);

            Console.WriteLine("Characters created!");
            Console.WriteLine();

            Console.WriteLine("Warrior: Aragorn");
            Console.WriteLine("Wizard: Gandalf");
            Console.WriteLine("Rogue: Robin");

            Pause();
        }

        static void ViewParty(
            AdventureGuild.Party.Party party)
        {
            Console.Clear();

            Console.WriteLine("===========================");
            Console.WriteLine("          PARTY");
            Console.WriteLine("===========================");
            Console.WriteLine();

            if (party.Characters.Count == 0)
            {
                Console.WriteLine("No characters have been created yet.");
                Pause();
                return;
            }

            foreach (Character character in party.Characters)
            {
                Console.WriteLine(
                    $"{character.Name} - " +
                    $"Health: {character.CurrentHealth}/{character.MaxHealth}");
            }

            Pause();
        }

        static void ViewInventory(
            AdventureGuild.Party.Party party)
        {
            Console.Clear();

            Console.WriteLine("===========================");
            Console.WriteLine("        INVENTORY");
            Console.WriteLine("===========================");
            Console.WriteLine();

            if (party.Characters.Count == 0)
            {
                Console.WriteLine("Create characters first.");
                Pause();
                return;
            }

            foreach (Character character in party.Characters)
            {
                Console.WriteLine($"{character.Name}:");

                if (character.Inventory.Count == 0)
                {
                    Console.WriteLine("  No items.");
                }
                else
                {
                    foreach (Item item in character.Inventory)
                    {
                        Console.WriteLine($"  - {item.Name}");

                        if (item is Weapon weapon)
                        {
                            Console.WriteLine("    Type: Weapon");
                            Console.WriteLine($"    Damage: {weapon.Damage}");
                        }
                        else if (item is Armor armor)
                        {
                            Console.WriteLine("    Type: Armor");
                            Console.WriteLine($"    Defense: {armor.Defense}");
                        }
                        else if (item is Potion potion)
                        {
                            Console.WriteLine("    Type: Potion");
                            Console.WriteLine($"    Healing: {potion.HealingAmount}");
                        }
                    }
                }
                Console.WriteLine("===========================");
            }

            Pause();
        }

        static void StartBattle(
            AdventureGuild.Party.Party party,
            RandomDiceRoller diceRoller)
        {
            Console.Clear();

            if (party.Characters.Count == 0)
            {
                Console.WriteLine("You need to create characters first.");
                Pause();
                return;
            }

            Monster monster =
                new Monster("Goblin King", 150, 12);

            AdventureGuild.Battle.Battle battle =
                new AdventureGuild.Battle.Battle(
                    party,
                    monster,
                    diceRoller);

            battle.Start();

            Pause();
        }

        static void ShowGameInformation()
        {
            Console.Clear();

            Console.WriteLine("==========================="); 
            Console.WriteLine(" GAME INFORMATION"); 
            Console.WriteLine("===========================");
            Console.WriteLine();

            Console.WriteLine("The Golden Dice is a turn-based");
            Console.WriteLine("fantasy battle game.");
            Console.WriteLine();

            Console.WriteLine("Characters:");
            Console.WriteLine("- Aragorn - Warrior");
            Console.WriteLine("- Gandalf - Wizard");
            Console.WriteLine("- Robin - Rogue");
            Console.WriteLine();

            Console.WriteLine("Inventory:");
            Console.WriteLine("- Weapons: Sword, Magic Staff, Dagger");
            Console.WriteLine("- Armor: Shield");
            Console.WriteLine("- Potion: Health Potion");
            Console.WriteLine();

            Console.WriteLine("Enemy:");
            Console.WriteLine("- Goblin King");
            Console.WriteLine();

            Console.WriteLine("Goal:");
            Console.WriteLine("Defeat the Goblin King!");

            Pause();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}


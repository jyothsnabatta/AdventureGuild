using AdventureGuild.Characters;
using AdventureGuild.Dice;
using AdventureGuild.Items;
using AdventureGuild.Monsters;
using System;

namespace AdventureGuild
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // create the dice roller
            RandomDiceRoller diceRoller = new RandomDiceRoller();

            // Create party
            AdventureGuild.Party.Party party = new AdventureGuild.Party.Party();

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("======================================================");
                Console.WriteLine("                        THE GOLDEN DICE     ");
                Console.WriteLine("                        ADVENTURE GUILD     ");
                Console.WriteLine("======================================================");
                Console.WriteLine();
                Console.WriteLine("1. Create Characters ");
                Console.WriteLine("2. View Party");
                Console.WriteLine("3. View Inventory");
                Console.WriteLine("4. Start Battle");
                Console.WriteLine("5. Game Information");
                Console.WriteLine("6. Exit");
                Console.WriteLine();

                Console.WriteLine("Choose an option");
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
                        StartBattle(party,diceRoller);
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

        // Create characters

        static void CreateCharacters(AdventureGuild.Party.Party party)
        {
            Console.Clear();
            Console.WriteLine("================== CREATE CHARACTERS =======================");
            Console.WriteLine();

            // Prevent creating the same characters twice.
            if(party.Characters.Count > 0)
            {
                Console.WriteLine("Characters have already been created.");
                Pause();
                return;
            }
            Warrior warrior = new Warrior("Aragorn", 5, 100);
            Wizard wizard = new Wizard("Gandalf", 5, 80);
            Rogue rogue = new Rogue("Robin", 5, 90);

            
            // Add characters to the party
            party.AddCharacter(warrior);
            party.AddCharacter(wizard);
            party.AddCharacter(rogue);

            Console.WriteLine();
            Console.WriteLine("Characters created!");
            Console.WriteLine();

            Console.WriteLine("Warrior: Aragon");
            Console.WriteLine("Wizard: Gandalf");
            Console.WriteLine("Rogue: Robin");

            Pause();

        }

        // VIEW PARTY

        static void ViewParty(AdventureGuild.Party.Party party)
        {
            Console.Clear();

            Console.WriteLine("===================== PARTY ==================");
            Console.WriteLine();

            if (party.Characters.Count == 0)
            {
                Console.WriteLine("No characters have been created yet.");
                Pause();
                return;
            }

            foreach (Character character in party.Characters)
            {
                Console.WriteLine($"{character.Name} - Health: " + $"{character.CurrentHealth}/{character.MaxHealth}");
            }

            Pause();
        }

        // VIEW INVENTORY
        static void ViewInventory(AdventureGuild.Party.Party party)
            {  
            Console.Clear();

            Console.WriteLine("============== INVENTORY ===============");
            Console.WriteLine();

            if(party.Characters.Count == 0) 
                {
                    Console.WriteLine("Create characters first.");
                    Pause();
                    return;
                }
                foreach (Character character in party.Characters)
                {
                    Console.WriteLine($"{character.Name}:");

                    if(character.Inventory.Count == 0)
                    {
                        Console.WriteLine(" No items. ");
                    }
                    else
                    {
                        foreach (Item item in character.Inventory)
                        {
                            Console.WriteLine($"    -{item.Name}");
                        }
                    }

                    Console.WriteLine();
                }

                Pause();
            }

            // START BATTLE

            static void StartBattle(AdventureGuild.Party.Party party,RandomDiceRoller diceRoller)
            {
                Console.Clear();

                if(party.Characters.Count == 0)
                {
                    Console.WriteLine("You need to create charcaters first.");
                    Pause();
                    return;
                }

                Monster monster = new Monster("Goblin King", 150, 12);

                AdventureGuild.Battle.Battle battle = new AdventureGuild.Battle.Battle(party, monster, diceRoller);

                battle.Start();
                Pause();
            }

            // GAME INFORMATION

            static void ShowGameInformation()
            {
                Console.Clear();

                Console.WriteLine("============== GAME INFORMATION =============");
                Console.WriteLine();

                Console.WriteLine("The Golden Dice is a turn-based"); 
                Console.WriteLine("fantasy battle game."); 
                Console.WriteLine(); 
                Console.WriteLine("Your party contains:"); 
                Console.WriteLine("- Warrior"); 
                Console.WriteLine("- Wizard"); 
                Console.WriteLine("- Rogue");
                Console.WriteLine();
                Console.WriteLine("Your goal is to defeat the"); 
                Console.WriteLine("Goblin King."); 
                Console.WriteLine();
                Console.WriteLine("Each character has different"); 
                Console.WriteLine("abilities and attacks."); 
                Pause();
            }

            // PAUSE
            static void Pause() 
            { 
                Console.WriteLine();
                Console.WriteLine("Press ENTER to continue..."); 
                Console.ReadLine(); 
            }
    }
}

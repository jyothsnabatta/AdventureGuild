using AdventureGuild.Characters;
using AdventureGuild.Exceptions;
using AdventureGuild.Interfaces;
using AdventureGuild.Items;
using AdventureGuild.Monsters;
using System;

namespace AdventureGuild.Battle
{
    // Battle controls the fight between the party and the monster.
    public class Battle
    {
        // Stores the player's party.
        private AdventureGuild.Party.Party party;
        // Stores the enemy monster.
        private Monster monster;
        // Used to generate random numbers for the battle.
        private IDiceRoller diceRoller;

        // Constructor receives the party, monster and dice roller.
        public Battle( AdventureGuild.Party.Party party, Monster monster,IDiceRoller diceRoller)
        {
            this.party = party;
            this.monster = monster;
            this.diceRoller = diceRoller;
        }
        // Starts the battle.
        public void Start()
        {
            Console.WriteLine("===========================");
            Console.WriteLine("       BATTLE STARTS!");
            Console.WriteLine("===========================");
            Console.WriteLine();

            // Show information about the monster.
            Console.WriteLine($"Monster: {monster.Name}");
            Console.WriteLine(
                $"Health: {monster.CurrentHealth}/{monster.MaxHealth}");
            Console.WriteLine();

            int round = 1;

            // Continue while the monster and at least one character are alive.
            while (!monster.IsDefeated &&
                   party.GetMembers().Count > 0)
            {
                Console.WriteLine("===========================");
                Console.WriteLine($"        ROUND {round}");
                Console.WriteLine("===========================");
                Console.WriteLine();

                // Let all living characters take their turns.
                PlayerTurn();

                // Stop if the monster was defeated.
                if (monster.IsDefeated)
                {
                    break;
                }
                // Let the monster attack.
                MonsterTurn();

                Console.WriteLine();
                // Move to the next round.
                round++;
            }

            Console.WriteLine();

            // Show the result of the battle.
            if (monster.IsDefeated)
            {
                Console.WriteLine("VICTORY!");
                Console.WriteLine(
                    $"{monster.Name} has been defeated!");
            }
            else
            {
                Console.WriteLine("DEFEAT!");
                Console.WriteLine(
                    "All characters have been defeated.");
            }
        }
        // Handles the player's turn.
        private void PlayerTurn()
        {
            Console.WriteLine("===========================");
            Console.WriteLine("        PLAYER TURN");
            Console.WriteLine("===========================");
            Console.WriteLine();
            // Go through all living characters.
            foreach (Character character in party.GetMembers())
            {
                if (monster.IsDefeated)
                {
                    break;
                }

                // Show the character's health and mana.

                ShowCharacterStatus(character);

                bool validChoice = false;
                // Keep asking until the player makes a valid choice.
                while (!validChoice)
                {
                    try
                    {
                        ShowMenu(character);

                        Console.Write("Choose: ");
                        string choice = Console.ReadLine();

                        // Attack the monster.
                        if (choice == "1")
                        {
                            character.Attack(monster);
                            validChoice = true;
                        }

                        // Wizard Casts Fireball
                        else if (character is Wizard wizard &&
                                 choice == "2")
                        {
                            wizard.CastSpell(monster);
                            validChoice = true;
                        }

                        // Wizard Uses a Health Potion
                        else if (character is Wizard &&
                                 choice == "3")
                        {
                            UseHealthPotion(character);
                            validChoice = true;
                        }

                        // Warrior and Rogue can skip their turn.
                        else if (!(character is Wizard) &&
                                 choice == "2")
                        {
                            Console.WriteLine(
                                $"{character.Name} skips the turn.");

                            validChoice = true;
                        }

                        // Wizard can skip their turn.
                        else if (character is Wizard &&
                                 choice == "4")
                        {
                            Console.WriteLine($"{character.Name} skips the turn.");

                            validChoice = true;
                        }

                        // Handle an invalid menu choice.
                        else
                        {
                            Console.WriteLine("Please choose one of the options.");
                        }
                    }
                    // Handle insufficient mana.
                    catch (InsufficientManaException ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Error: {ex.Message}");

                        validChoice = true;
                    }
                    // Handle a defeated character.
                    catch (CharacterIsDefeatedException ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Error: {ex.Message}");

                        validChoice = true;
                    }
                    // Handle other errors.
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Error: {ex.Message}");

                        validChoice = true;
                    }
                }

                Console.WriteLine();
            }
        }
        // Displays the available actions for a character.
        private void ShowMenu(Character character)
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Attack");
            // Wizards have extra actions.
            if (character is Wizard)
            {
                Console.WriteLine("2. Cast Fireball");
                Console.WriteLine("3. Use Health Potion");
                Console.WriteLine("4. Skip");
            }
            else
            {
                Console.WriteLine("2. Skip");
            }

            Console.WriteLine();
        }

        // Displays the character's current status.
        private void ShowCharacterStatus(Character character)
        {
            Console.WriteLine(character.Name);

            Console.WriteLine(
                $"Health: {character.CurrentHealth}/{character.MaxHealth}");
            // Wizards also show their mana. 
            if (character is Wizard wizard)
            {
                Console.WriteLine(
                    $"Mana: {wizard.Mana}/{wizard.MaxMana}");
            }

            Console.WriteLine();
        }
        // Finds and uses a Health Potion.
        private void UseHealthPotion(Character character)
        {
            Potion potion = null;
            // Search the character's inventory for a potion.
            foreach (Item item in character.Inventory)
            {
                if (item is Potion)
                {
                    potion = (Potion)item;
                    break;
                }
            }
            // No potion was found.
            if (potion == null)
            {
                Console.WriteLine(
                    $"{character.Name} does not have a Health Potion.");
                return;
            }
            // Do not use a potion when health is already full.
            if (character.CurrentHealth == character.MaxHealth)
            {
                Console.WriteLine(
                    $"{character.Name} already has full health.");
                return;
            }
            // Use the potion to heal the character.
            potion.Use(character);

            // Remove the used potion from the inventory.
            character.RemoveItem(potion);

            Console.WriteLine(
                $"{potion.Name} has been removed from the inventory.");
        }
        // Handles the monster's turn.
        private void MonsterTurn()
        {
            // Do nothing if the monster is already defeated.
            if (monster.IsDefeated)
            {
                return;
            }
            // Get all living party members.
            var members = party.GetMembers();

            if (members.Count == 0)
            {
                return;
            }
            // Randomly choose a character to attack.
            int index = diceRoller.Roll(0, members.Count - 1);

            Character target = members[index];

            Console.WriteLine("===========================");
            Console.WriteLine("        MONSTER TURN");
            Console.WriteLine("===========================");
            Console.WriteLine();

            try
            {
                // Monster attacks the selected character.
                monster.Attack(target);
            }
            // Handle a defeated character.
            catch (CharacterIsDefeatedException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            // Handle other errors.
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}


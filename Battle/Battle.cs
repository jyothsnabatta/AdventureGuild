using AdventureGuild.Characters;
using AdventureGuild.Exceptions;
using AdventureGuild.Interfaces;
using AdventureGuild.Items;
using AdventureGuild.Monsters;
using System;

namespace AdventureGuild.Battle
{
    public class Battle
    {
        private AdventureGuild.Party.Party party;
        private Monster monster;
        private IDiceRoller diceRoller;

        public Battle(
            AdventureGuild.Party.Party party,
            Monster monster,
            IDiceRoller diceRoller)
        {
            this.party = party;
            this.monster = monster;
            this.diceRoller = diceRoller;
        }

        public void Start()
        {
            Console.WriteLine("===========================");
            Console.WriteLine("       BATTLE STARTS!");
            Console.WriteLine("===========================");
            Console.WriteLine();

            Console.WriteLine($"Monster: {monster.Name}");
            Console.WriteLine(
                $"Health: {monster.CurrentHealth}/{monster.MaxHealth}");
            Console.WriteLine();

            int round = 1;

            while (!monster.IsDefeated &&
                   party.GetMembers().Count > 0)
            {
                Console.WriteLine("===========================");
                Console.WriteLine($"        ROUND {round}");
                Console.WriteLine("===========================");
                Console.WriteLine();

                PlayerTurn();

                if (monster.IsDefeated)
                {
                    break;
                }

                MonsterTurn();

                Console.WriteLine();
                
                round++;
            }

            Console.WriteLine();

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

        private void PlayerTurn()
        {
            Console.WriteLine("===========================");
            Console.WriteLine("        PLAYER TURN");
            Console.WriteLine("===========================");
            Console.WriteLine();

            foreach (Character character in party.GetMembers())
            {
                if (monster.IsDefeated)
                {
                    break;
                }

                ShowCharacterStatus(character);

                bool validChoice = false;

                while (!validChoice)
                {
                    try
                    {
                        ShowMenu(character);

                        Console.Write("Choose: ");
                        string choice = Console.ReadLine();

                        // Attack
                        if (choice == "1")
                        {
                            character.Attack(monster);
                            validChoice = true;
                        }

                        // Wizard: Cast Fireball
                        else if (character is Wizard wizard &&
                                 choice == "2")
                        {
                            wizard.CastSpell(monster);
                            validChoice = true;
                        }

                        // Gandalf: Use Health Potion
                        else if (character is Wizard &&
                                 choice == "3")
                        {
                            UseHealthPotion(character);
                            validChoice = true;
                        }

                        // Warrior/Rogue: Skip
                        else if (!(character is Wizard) &&
                                 choice == "2")
                        {
                            Console.WriteLine(
                                $"{character.Name} skips the turn.");

                            validChoice = true;
                        }

                        // Wizard: Skip
                        else if (character is Wizard &&
                                 choice == "4")
                        {
                            Console.WriteLine(
                                $"{character.Name} skips the turn.");

                            validChoice = true;
                        }

                        // Wrong choice
                        else
                        {
                            Console.WriteLine(
                                "Please choose one of the options.");
                        }
                    }
                    catch (InsufficientManaException ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Error: {ex.Message}");

                        validChoice = true;
                    }
                    catch (CharacterIsDefeatedException ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Error: {ex.Message}");

                        validChoice = true;
                    }
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

        private void ShowMenu(Character character)
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Attack");

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

        private void ShowCharacterStatus(Character character)
        {
            Console.WriteLine(character.Name);

            Console.WriteLine(
                $"Health: {character.CurrentHealth}/{character.MaxHealth}");

            if (character is Wizard wizard)
            {
                Console.WriteLine(
                    $"Mana: {wizard.Mana}/{wizard.MaxMana}");
            }

            Console.WriteLine();
        }

        private void UseHealthPotion(Character character)
        {
            Potion potion = null;

            foreach (Item item in character.Inventory)
            {
                if (item is Potion)
                {
                    potion = (Potion)item;
                    break;
                }
            }

            if (potion == null)
            {
                Console.WriteLine(
                    $"{character.Name} does not have a Health Potion.");
                return;
            }

            if (character.CurrentHealth == character.MaxHealth)
            {
                Console.WriteLine(
                    $"{character.Name} already has full health.");
                return;
            }

            potion.Use(character);

            character.RemoveItem(potion);

            Console.WriteLine(
                $"{potion.Name} has been removed from the inventory.");
        }

        private void MonsterTurn()
        {
            if (monster.IsDefeated)
            {
                return;
            }

            var members = party.GetMembers();

            if (members.Count == 0)
            {
                return;
            }

            int index =
                diceRoller.Roll(0, members.Count - 1);

            Character target = members[index];

            Console.WriteLine("===========================");
            Console.WriteLine("        MONSTER TURN");
            Console.WriteLine("===========================");
            Console.WriteLine();

            try
            {
                monster.Attack(target);
            }
            catch (CharacterIsDefeatedException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}


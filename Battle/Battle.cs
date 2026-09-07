using AdventureGuild.Characters;
using AdventureGuild.Exceptions;
using AdventureGuild.Interfaces;
using AdventureGuild.Monsters;
using System;


namespace AdventureGuild.Battle
{
    public class Battle
    {
        private AdventureGuild.Party.Party party;
        private Monster monster;
        private IDiceRoller diceRoller;

        public Battle(AdventureGuild.Party.Party party, Monster monster, IDiceRoller diceRoller)
        {
            if( party == null )
            {
                throw new ArgumentNullException(nameof(party));
            }
            if( monster == null )
            { 
                throw new ArgumentNullException(nameof(monster));
            }
            if ( diceRoller == null )
            {
                throw new ArgumentNullException(nameof (diceRoller));   
            }
            this.party = party;
            this.monster = monster;
            this.diceRoller = diceRoller;
        }

        public void Start() 
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine("                        BATTLE STARTS!                   ");
            Console.WriteLine("===============================================================");

            Console.WriteLine($"Monster: {monster.Name}");
            Console.WriteLine($"Health: {monster.CurrentHealth}/{monster.MaxHealth}");
            Console.WriteLine();

            while (!monster.IsDefeated && party.GetMembers().Count > 0)
            {
                Console.WriteLine("-------------------------PLAYER TURN---------------------------");

                foreach(Character character in party.GetMembers())
                {
                    if (monster.IsDefeated) 
                    {
                        break;
                    }
                    try
                    {
                        if (character.IsDefeated) 
                        {
                            throw new CharacterIsDefeatedException($"{character.Name} is already defeated.");
                        }

                        // Wizard uses a spell

                        if (character is Wizard wizard)
                        {
                            wizard.CastSpell(monster);
                        }
                        else
                        {
                            character.Attack(monster);
                        }
                        Console.WriteLine();
                    }
                    catch (CharacterIsDefeatedException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    catch (InsufficientManaException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine($"{character.Name} uses a normal attack instead.");

                        character.Attack(monster);
                    }
                }
                if (monster.IsDefeated)
                { 
                    Console.WriteLine();
                    Console.WriteLine($"Victory! {monster.Name} has been defeated!");
                    break;
                }

                 Console.WriteLine("-------------------------MONSTER TURN--------------------------");

                try
                {
                    var livingCharacters = party.GetMembers();

                    if (livingCharacters.Count == 0)
                    {
                        break;
                    }

                    int roll = diceRoller.Roll(0, livingCharacters.Count - 1);
                    Character target = livingCharacters[roll];

                    monster.Attack(target);
                    Console.WriteLine();
                    Console.WriteLine();

                }

                catch (CharacterIsDefeatedException ex) 
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("===============================================================");
            Console.WriteLine("                        BATTLE ENDS!                   ");
            Console.WriteLine("===============================================================");

        }

    }

}






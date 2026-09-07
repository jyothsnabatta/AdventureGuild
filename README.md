# Adventure Guild

The Golden Dice — Adventure Guild is a small, text-based, turn-based fantasy battle console game written in C#.

It demonstrates basic object-oriented design with characters (Warrior, Wizard, Rogue), items (Weapons, Armor, Potions), monsters, a party system, and a simple battle loop driven by dice rolls.

## Features

- Create a party of three characters (Warrior, Wizard, Rogue)
- Simple inventory system with weapons, armor, and potions
- Turn-based battle against a Monster (Goblin King)
- Extensible project layout demonstrating separation of concerns across folders

## Requirements

- Windows or any environment capable of building .NET Framework 4.8 projects
- Visual Studio 2019/2022 (recommended) or MSBuild that supports .NET Framework 4.8

> Note: This project targets .NET Framework 4.8 (see AdventureGuild.csproj). If you prefer the cross-platform `dotnet` CLI, consider retargeting the project to .NET 6+ or .NET 7.

## Build and Run

1. Open `AdventureGuild.sln` in Visual Studio and build the solution, then run the project.
2. Or build from the command line using MSBuild:

   msbuild AdventureGuild.sln /p:Configuration=Debug

3. The game runs as a console application. Use the displayed menu to create characters, view the party and inventory, and start battles.

## Usage / Controls

When the program runs you will see a menu with options:

1. Create Characters — creates a preconfigured Warrior (Aragorn), Wizard (Gandalf), and Rogue (Robin) and their starter items
2. View Party — lists party members and their health
3. View Inventory — lists each character's items and item details
4. Start Battle — begins a turn-based battle against the Goblin King
5. Game Information — shows a summary of characters, inventory and enemy
6. Exit — quit the game

Press ENTER when prompted to continue between screens.

## Project Structure

- Battle/ — battle engine and logic (Battle.cs)
- Characters/ — Character base class and concrete classes (Warrior, Wizard, Rogue)
- Dice/ — dice-rolling abstraction and implementation (RandomDiceRoller)
- Exceptions/ — custom exceptions used by game logic
- Interfaces/ — interfaces like IDamageable, IDiceRoller, ISpellcaster
- Items/ — Item, Weapon, Armor, Potion classes
- Monsters/ — Monster class
- Party/ — Party management
- Program.cs — console UI and game flow
- AdventureGuild.csproj, AdventureGuild.sln — project/solution files

## Extending the Game

- Add more monster types and behaviors
- Implement equipment effects and stats progression
- Add save/load support
- Retarget to .NET Core/NET 6+ for cross-platform support

## Contributing

Contributions are welcome. Please open issues for feature requests or bugs and submit pull requests for changes.

## License

No license specified. If you want to attach an open-source license, add a LICENSE file.

---

Original repository: https://github.com/jyothsnabatta/AdventureGuild



namespace AdventureGuild.Interfaces
{
    // Defines the contract for objects that can perform dice rolls.
    // Different dice-rolling implementations can follow this interface.
    public interface IDiceRoller
    {

        // Rolls a value between the specified minimum and maximum values.
        int Roll(int min, int max);
    }
}

using AdventureGuild.Characters;


namespace AdventureGuild.Interfaces
{

    // Defines the behavior required for objects that can cast spells.
    // Any class implementing this interface must provide its own
    // implementation of CastSpell.
    public interface ISpellcaster
    {

        // Casts a spell against a target that can receive damage.
        // IDamageable keeps the interface loosely coupled to specific target types.
        void CastSpell(IDamageable target);
       
    }
}


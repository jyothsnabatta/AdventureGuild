using AdventureGuild.Characters;


namespace AdventureGuild.Interfaces
{
    public interface ISpellcaster
    {
       void CastSpell(IDamageable target);
       
    }
}


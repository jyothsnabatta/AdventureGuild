

namespace AdventureGuild.Interfaces
{

    // Defines the common behavior for objects that can receive damage.
    // Any class implementing this interface must provide its own
    // implementation of TakeDamage.
    public interface IDamageable
    {

        // Reduces the object's health by the specified damage amount.
        void TakeDamage(int damage);
    }
}

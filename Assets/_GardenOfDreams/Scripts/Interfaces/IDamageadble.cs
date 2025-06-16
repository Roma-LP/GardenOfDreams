using System;

namespace _GardenOfDreams.Scripts.Interfaces
{
    public interface IDamageable
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }

        event Action<float, float> OnHealthChanged; // (currentHealth, maxHealth)

        void TakeDamage(float amount);
        void Die();
    }
}
using System;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Interfaces
{
    public interface IDamageable
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }
        Transform TargetTransform { get; }

        event Action<float, float> OnHealthChanged; // (currentHealth, maxHealth)
        event Action<float> OnTakeDamage;

        void TakeDamage(float amount);
    }
}
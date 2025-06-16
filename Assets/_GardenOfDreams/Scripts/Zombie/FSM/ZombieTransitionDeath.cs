using _GardenOfDreams.Scripts.StateMachineStuff;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Zombie.FSM
{
    public class ZombieTransitionDeath : FSMTransition
    {
        [SerializeField] private ZombieUnit _zombieUnit;

        protected override void OnEnable()
        {
            base.OnEnable();

            _zombieUnit.OnHealthChanged += HealthChangedHandler;
        }

        private void OnDisable()
        {
            _zombieUnit.OnHealthChanged -= HealthChangedHandler;
        }

        private void HealthChangedHandler(float currentHealth, float maxHealth)
        {
            if (currentHealth <= 0f)
                NeedTransit = true;
        }
    }
}
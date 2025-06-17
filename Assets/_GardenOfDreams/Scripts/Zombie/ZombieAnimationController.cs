using System;
using _GardenOfDreams.Scripts.Player;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Zombie
{
    public class ZombieAnimationController : PersonAnimationController
    {
        private readonly int ATTACK = Animator.StringToHash("Attack");

        public event Action OnAttackMoment;
        public event Action OnAttackAnimationEnd;
        
        public void SetAttack()
        {
            _animator.SetTrigger(ATTACK);
        }

        public void TriggerAttackMoment()
        {
            OnAttackMoment?.Invoke();
        }
        
        public void TriggerAttackAnimationEnd()
        {
            OnAttackAnimationEnd?.Invoke();
        }
    }
}

using System;
using _GardenOfDreams.Scripts.Interfaces;
using UnityEngine;
using _GardenOfDreams.Scripts.Zombie;

namespace _GardenOfDreams.Scripts.Player
{
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _circleCollider2D;

        public event Action<IDamageable> OnEnemyEntered;
        public event Action<IDamageable> OnEnemyExited;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable zombie))
            {
                OnEnemyEntered?.Invoke(zombie);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable zombie))
            {
                OnEnemyExited?.Invoke(zombie);
            }
        }
    }
}
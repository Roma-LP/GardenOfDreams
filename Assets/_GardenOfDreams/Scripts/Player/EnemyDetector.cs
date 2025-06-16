using System;
using UnityEngine;
using _GardenOfDreams.Scripts.Zombie;

namespace _GardenOfDreams.Scripts.Player
{
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _circleCollider2D;

        public event Action<ZombieUnit> OnEnemyEntered;
        public event Action<ZombieUnit> OnEnemyExited;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out ZombieUnit zombie))
            {
                OnEnemyEntered?.Invoke(zombie);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out ZombieUnit zombie))
            {
                OnEnemyExited?.Invoke(zombie);
            }
        }
    }
}
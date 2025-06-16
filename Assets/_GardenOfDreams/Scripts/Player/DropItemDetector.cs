using System;
using _GardenOfDreams.Scripts.Interfaces;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class DropItemDetector : MonoBehaviour
    {
        public event Action<IPickable> OnDropItemEntered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IPickable pickable))
            {
                OnDropItemEntered?.Invoke(pickable);
            }
        }
    }
}

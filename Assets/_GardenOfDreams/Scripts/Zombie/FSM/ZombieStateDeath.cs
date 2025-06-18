using _GardenOfDreams.Scripts.StateMachineStuff;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Zombie.FSM
{
    public class ZombieStateDeath : FSMState
    {
        [SerializeField] private ZombieUnit _zombieUnit;
        [SerializeField] private DropItem _dropItem;

        private void OnEnable()
        {
            SetLinks();
            
            DropItem dropItem = Instantiate(_dropItem, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
        
        private void SetLinks()
        {
            if (_dropItem != null)
                return;
            
            _dropItem = _zombieUnit.ZombieLinks.DropItemWhenDies;
        }
    }
}
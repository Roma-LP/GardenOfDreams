using _GardenOfDreams.Scripts.StateMachineStuff;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Zombie.FSM
{
    public class ZombieStateDeath : FSMState
    {
        [SerializeField] private DropItem _dropItem;

        private void OnEnable()
        {
            DropItem dropItem = Instantiate(_dropItem, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
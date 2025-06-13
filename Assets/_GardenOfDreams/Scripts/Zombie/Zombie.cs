using UnityEngine;
using UnityEngine.AI;

namespace _GardenOfDreams.Scripts.Zombie
{
    public class Zombie : MonoBehaviour
    {
        [SerializeField] private ZombieAnimationController _animation;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private ZombieFSM _zombieFsm;

        public NavMeshAgent Agent => _agent;

        private void Awake()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void Update()
        {
            _zombieFsm.UpdateFSM();
            _animation.SetSpeed(new Vector2(_agent.velocity.x, _agent.velocity.y));
        }

        public void MoveTo(Vector3 target)
        {
            _agent.SetDestination(target);
        }

        public void StopMoving()
        {
            _agent.ResetPath();
            _animation.SetSpeed(Vector2.zero);
        } 
    }
}
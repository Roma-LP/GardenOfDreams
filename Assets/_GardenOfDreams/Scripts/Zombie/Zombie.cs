using UnityEngine;
using UnityEngine.AI;

namespace _GardenOfDreams.Scripts.Zombie
{
    public class Zombie : MonoBehaviour
    {
        [SerializeField] private ZombieAnimationController _animation;
        [SerializeField] private NavMeshAgent _agent;

        public NavMeshAgent Agent => _agent;
        
        public void MoveTo(Vector3 target)
        {
            _agent.SetDestination(target);
            _animation.SetSpeed(new Vector2(_agent.velocity.x, _agent.velocity.y));
        }

        public void StopMoving()
        {
            _agent.ResetPath();
            _animation.SetSpeed(Vector2.zero);
        } 
    }
}
using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.Player;
using _GardenOfDreams.Scripts.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace _GardenOfDreams.Scripts.Zombie
{
    public class ZombieUnit : EnemyBase, ISpawnable<ZombieLinks>
    {
        [SerializeField] private ZombieAnimationController _animation;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private ZombieFSM _zombieFsm;

        private PlayerUnit _playerUnit;
        private ZombieLinks _zombieLinks;
        
        public NavMeshAgent Agent => _agent;
        public PlayerUnit PlayerUnit => _playerUnit;
        public ZombieAnimationController ZombieAnimation => _animation;
        public ZombieLinks ZombieLinks => _zombieLinks;

        protected override void Awake()
        {
            base.Awake();
            
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            _playerUnit = SceneContext.Instance.PlayerUnit;
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

        public void OnSpawned(ZombieLinks parametrs)
        {
            _zombieLinks = parametrs;
        }
    }
}
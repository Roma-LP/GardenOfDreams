using _GardenOfDreams.Scripts.StateMachineStuff;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Zombie.FSM
{
    public class ZombieStateAttack : FSMState
    {
        [SerializeField] private ZombieUnit _zombieUnit;
        [SerializeField] private float _attackDistance = 0.2f;
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _attackDamage = 20f;

        private float _cooldownTimer;
        private bool _isAttackAnimationIsPlaying;
        private bool _isInAttackDistance;

        private void OnEnable()
        {
            _cooldownTimer = 0;
            _isAttackAnimationIsPlaying = false;

            _zombieUnit.ZombieAnimation.OnAttackAnimationEnd += AttackAnimationEndHandler;
            _zombieUnit.ZombieAnimation.OnAttackMoment += AttackMomentHandler;
        }

        private void OnDisable()
        {
            
            _zombieUnit.ZombieAnimation.OnAttackAnimationEnd -= AttackAnimationEndHandler;
            _zombieUnit.ZombieAnimation.OnAttackMoment -= AttackMomentHandler;
        }

        public override void UpdateState()
        {
            if (_isAttackAnimationIsPlaying)
                return;
            
            float distance = Vector3.Distance(_zombieUnit.transform.position, _zombieUnit.PlayerUnit.transform.position);

            if (distance > _attackDistance)
            {
                _zombieUnit.MoveTo(_zombieUnit.PlayerUnit.transform.position);
                _isInAttackDistance = false;
            }
            else
            {
                _zombieUnit.StopMoving();
                _isInAttackDistance = true;

                if (_cooldownTimer <= 0f)
                {
                    StartAttack();
                }
                else
                {
                    _cooldownTimer -= Time.deltaTime;
                }
            }
        }

        private void StartAttack()
        {
            _isAttackAnimationIsPlaying = true;
            _zombieUnit.ZombieAnimation.SetAttack();
        }

        private void AttackAnimationEndHandler()
        {
            _cooldownTimer = _attackCooldown;
            _isAttackAnimationIsPlaying = false;
        }
        
        private void AttackMomentHandler()
        {
            if(_isInAttackDistance == false)
                return;
            
            _zombieUnit.PlayerUnit.TakeDamage(_attackDamage);
        }
    }
}
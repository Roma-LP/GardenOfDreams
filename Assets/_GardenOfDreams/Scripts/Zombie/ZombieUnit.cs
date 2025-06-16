using System;
using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.UI.HP;
using _GardenOfDreams.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace _GardenOfDreams.Scripts.Zombie
{
    public class ZombieUnit : MonoBehaviour, IDamageable
    {
        [SerializeField] private ZombieAnimationController _animation;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private ZombieFSM _zombieFsm;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private Transform _pivotUI;

        [ShowInInspector, ReadOnly] private float _currentHealth;
        private HealthBarUI _healthBarUI;
        private WorldToUIFollower _worldToUIFollower;
        
        public event Action<float, float> OnHealthChanged;
        
        public NavMeshAgent Agent => _agent;
        public float MaxHealth => _maxHealth;
        public Transform TargetTransform => transform;

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

                OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
                _healthBarUI.SetHealth(_currentHealth);
            }
        }
        
        private void Awake()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            _currentHealth = _maxHealth;
            _healthBarUI = SceneContext.Instance.WorldHpBarSpawner.CreateHpBar(_maxHealth);
            _worldToUIFollower = new WorldToUIFollower(_healthBarUI.RectTransformToMove, _pivotUI);
        }

        private void Update()
        {
            _zombieFsm.UpdateFSM();
            _animation.SetSpeed(new Vector2(_agent.velocity.x, _agent.velocity.y));
        }

        private void LateUpdate()
        {
            _worldToUIFollower.LateUpdateUIFollower();
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
        
        public void TakeDamage(float amount)
        {
            CurrentHealth -= amount;
        }

        private void OnDestroy()
        {
            Destroy(_healthBarUI.gameObject);
        }
    }
}
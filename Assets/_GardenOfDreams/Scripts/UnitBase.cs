using System;
using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.UI.HP;
using _GardenOfDreams.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GardenOfDreams.Scripts
{
    public abstract class EnemyBase : MonoBehaviour, IDamageable
    {
        [ShowInInspector, ReadOnly] private float _currentHealth;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private Transform _pivotUI;

        private HealthBarUI _healthBarUI;
        private WorldToUIFollower _worldToUIFollower;

        public event Action<float, float> OnHealthChanged;

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

        public float MaxHealth => _maxHealth;
        public Transform TargetTransform => transform;

        protected virtual void Awake()
        {
            _currentHealth = _maxHealth;
            _healthBarUI = SceneContext.Instance.WorldHpBarSpawner.CreateHpBar(_maxHealth);
            _worldToUIFollower = new WorldToUIFollower(_healthBarUI.RectTransformToMove, _pivotUI);
        }

        protected virtual void LateUpdate()
        {
            _worldToUIFollower.LateUpdateUIFollower();
        }

        protected virtual void OnDestroy()
        {
            if (_healthBarUI is not null)
                Destroy(_healthBarUI.gameObject);
        }

        public void TakeDamage(float amount)
        {
            CurrentHealth -= amount;
        }
    }
}
using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.Utilities;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class LookTargetController : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _lookTarget;
        [SerializeField] private float radius = 1.5f;
        [SerializeField] private EnemyDetector _enemyDetector;

        private Vector2 _lastDirection = Vector2.right;
        private OrderedSet<IDamageable> _damageablesUnits = new OrderedSet<IDamageable>();
        private Joystick _joystick;

        public IDamageable LastDamageablesUnit => _damageablesUnits.Last;
        
        public void Init()
        {
            _joystick = SceneContext.Instance.PlayerUIInput.Joystick;
            
            _enemyDetector.OnEnemyEntered += EnemyEnteredHandler;
            _enemyDetector.OnEnemyExited += EnemyExitedHandler;
        }

        private void OnDestroy()
        {
            _enemyDetector.OnEnemyEntered -= EnemyEnteredHandler;
            _enemyDetector.OnEnemyExited -= EnemyExitedHandler;
        }

        private void EnemyEnteredHandler(IDamageable zombieUnit)
        {
            if (_damageablesUnits.Add(zombieUnit))
            {
            }
        }

        private void EnemyExitedHandler(IDamageable zombieUnit)
        {
            if (_damageablesUnits.Remove(zombieUnit))
            {
            }
        }

        public void UpdateLookTarget()
        {
            if (_damageablesUnits.IsEmpty)
            {
                Vector2 direction = _joystick.Direction;

                if (direction.sqrMagnitude > 0.01f)
                {
                    _lastDirection = direction.normalized;
                }

                Vector3 offset = new Vector3(_lastDirection.x, _lastDirection.y, 0) * radius;
                _lookTarget.position = _player.position + offset;
            }
            else
            {
                _lookTarget.position = LastDamageablesUnit.TargetTransform.position;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_player == null)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_player.position, radius);
        }
    }
}
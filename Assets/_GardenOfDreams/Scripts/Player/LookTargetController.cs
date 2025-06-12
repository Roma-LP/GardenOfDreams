using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class LookTargetController : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _lookTarget;
        [SerializeField] private float radius = 1.5f;

        private Vector2 _lastDirection = Vector2.right;

        public void UpdateLookTarget()
        {
            Vector2 direction = _joystick.Direction;

            if (direction.sqrMagnitude > 0.01f)
            {
                _lastDirection = direction.normalized;
            }

            Vector3 offset = new Vector3(_lastDirection.x, _lastDirection.y, 0) * radius;
            _lookTarget.position = _player.position + offset;
        }
        
        private void OnDrawGizmos()
        {
            if (_player == null)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_player.position, radius);
        }
    }
}
using UnityEngine;

namespace _GardenOfDreams.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _moveSpeed = 3;
        [SerializeField] private Joystick _joystick;

        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            _rigidbody2D.velocity = direction * _moveSpeed;
        }
    }
}

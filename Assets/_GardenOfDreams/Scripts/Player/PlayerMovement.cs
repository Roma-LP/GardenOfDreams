using _GardenOfDreams.Scripts.Utilities;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _moveSpeed = 3;
        [SerializeField] private PlayerAnimationController _animator;

        private Vector2 _moveInput;
        private Joystick _joystick;

        public void Init()
        {
            _joystick = SceneContext.Instance.PlayerUIInput.Joystick;
        }   
        
        public void UpdateMovement()
        {
            _animator.SetSpeed(_moveInput);
        }

        private void FixedUpdate()
        {
            _moveInput = _joystick.Direction;
            _rigidbody2D.velocity = _moveInput * _moveSpeed;
        }
    }
}

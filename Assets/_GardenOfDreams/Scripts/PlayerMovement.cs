using System;
using UnityEngine;

namespace _GardenOfDreams.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _moveSpeed = 3;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerAnimationController _animator;

        private Vector2 _moveInput;
        
        private void Update()
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

using UnityEngine;

namespace _GardenOfDreams.Scripts
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _rootPlayer;

        private readonly int SPEED = Animator.StringToHash("Speed");
        
        private bool _facingLeft = false;

        public void SetSpeed(Vector2 moveInput)
        {
            _animator.SetFloat(SPEED, moveInput.magnitude);

            _animator.SetFloat(SPEED, moveInput.magnitude);

            if (moveInput.x < -0.01f && !_facingLeft)
            {
                Flip(true);
            }
            else if (moveInput.x > 0.01f && _facingLeft)
            {
                Flip(false);
            }
        }

        private void Flip(bool faceLeft)
        {
            Vector3 scale = _rootPlayer.localScale;
            scale.x = Mathf.Abs(scale.x) * (faceLeft ? -1 : 1);
            _rootPlayer.localScale = scale;
            _facingLeft = faceLeft;
        }
    }
}
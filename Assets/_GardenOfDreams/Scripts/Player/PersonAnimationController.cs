using UnityEngine;
using UnityEngine.Serialization;

namespace _GardenOfDreams.Scripts.Player
{
    public abstract class PersonAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _rootPerson;
        [SerializeField, Range(0.001f,0.1f)] private float _flipThreshold = 0.01f;

        private readonly int SPEED = Animator.StringToHash("Speed");
        
        private bool _facingLeft = false;

        public void SetSpeed(Vector2 moveInput)
        {
            _animator.SetFloat(SPEED, moveInput.magnitude);

            SetFacingDirection(moveInput);
        }

        protected virtual void SetFacingDirection(Vector2 moveInput)
        {
            if (moveInput.x < -_flipThreshold && !_facingLeft)
            {
                Flip(true);
            }
            else if (moveInput.x > _flipThreshold && _facingLeft)
            {
                Flip(false);
            }
        }

        private void Flip(bool faceLeft)
        {
            Vector3 scale = _rootPerson.localScale;
            scale.x = Mathf.Abs(scale.x) * (faceLeft ? -1 : 1);
            _rootPerson.localScale = scale;
            _facingLeft = faceLeft;
        }
    }
}
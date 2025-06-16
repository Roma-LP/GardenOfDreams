using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class PlayerAnimationController : PersonAnimationController
    {
        [SerializeField] private Transform _lookTarget;

        protected override void SetFacingDirection(Vector2 _)
        {
            Vector2 direction = _lookTarget.position - transform.position;
            base.SetFacingDirection(direction);
        }
    }
}
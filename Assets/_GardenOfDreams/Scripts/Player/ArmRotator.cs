using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class ArmRotator : MonoBehaviour
    {
        [SerializeField] private Transform _leftArm;
        [SerializeField] private Transform _lookTarget;

        public void UpdateArm()
        {
            Vector2 direction = _lookTarget.position - _leftArm.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _leftArm.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        }
    }
}
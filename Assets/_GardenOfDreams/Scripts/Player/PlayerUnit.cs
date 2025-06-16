using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private LookTargetController _lookTargetController;
        [SerializeField] private ArmRotator _armRotator;

        private void Awake()
        {
            _lookTargetController.Init();
        }

        private void Update()
        {
            _playerMovement.UpdateMovement();
            _lookTargetController.UpdateLookTarget();
            _armRotator.UpdateArm();
        }
    }
}
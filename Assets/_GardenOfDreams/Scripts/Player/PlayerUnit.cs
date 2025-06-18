using _GardenOfDreams.Scripts.UI.Inventory;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class PlayerUnit : UnitBase
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private LookTargetController _lookTargetController;
        [SerializeField] private ArmRotator _armRotator;
        [SerializeField] private DropItemHandler _dropItemHandler;
        [Header("Fight Settings")]
        [SerializeField] private float _damage = 25;
        [SerializeField] private InventoryItem _inventoryItem = InventoryItem.Ammunition;

        private PlayerUIInputHandler _playerUIInput;
        
        protected override void Awake()
        {
            base.Awake();
            
            _lookTargetController.Init();
            _dropItemHandler.Init();
            _playerMovement.Init();

            _playerUIInput = new PlayerUIInputHandler(_lookTargetController, _damage, _inventoryItem);
        }

        private void Update()
        {
            _playerMovement.UpdateMovement();
            _lookTargetController.UpdateLookTarget();
            _armRotator.UpdateArm();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _playerUIInput.Dispose();
        }
    }
}
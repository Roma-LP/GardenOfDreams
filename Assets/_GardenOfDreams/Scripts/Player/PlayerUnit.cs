using _GardenOfDreams.Scripts.SaveTools.Models;
using _GardenOfDreams.Scripts.UI.Inventory;
using _GardenOfDreams.Scripts.Utilities;
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
        private PlayerDataUpdateTrigger _playerDataUpdateTrigger;
        
        protected override void Awake()
        {
            base.Awake();
            
            _lookTargetController.Init();
            _dropItemHandler.Init();
            _playerMovement.Init();

            _playerUIInput = new PlayerUIInputHandler(_lookTargetController, _damage, _inventoryItem);
            _playerDataUpdateTrigger = new PlayerDataUpdateTrigger(this);
        }

        private void Update()
        {
            _playerMovement.UpdateMovement();
            _lookTargetController.UpdateLookTarget();
            _armRotator.UpdateArm();
        }

        protected override float GetInitialHealth()
        {
            PlayerData playerData = SceneContext.Instance.ProjectDatasContainer.PlayerData;

            if (playerData.TryGetHealth(out float health))
            {
                return health;
            }
            else
            {
                return base.GetInitialHealth();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _playerUIInput.Dispose();
            _playerDataUpdateTrigger.Dispose();
        }
    }
}
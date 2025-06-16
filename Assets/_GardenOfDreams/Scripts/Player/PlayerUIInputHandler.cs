using System;
using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.UI.Inventory;
using _GardenOfDreams.Scripts.UI.PlayerInput;
using _GardenOfDreams.Scripts.Utilities;

namespace _GardenOfDreams.Scripts.Player
{
    public class PlayerUIInputHandler : IDisposable
    {
        private const byte COUNT_TO_REMOVE_AMMUNITION = 1;
        
        private PlayerUIInput _playerUIInput;
        private InventoryController _inventoryController;
        private LookTargetController _lookTargetController;
        private float _damage;
        private InventoryItem _inventoryItem;

        public PlayerUIInputHandler(LookTargetController lookTargetController, float damage,
            InventoryItem inventoryItem)
        {
            _playerUIInput = SceneContext.Instance.PlayerUIInput;
            _lookTargetController = lookTargetController;
            _damage = damage;
            _inventoryItem = inventoryItem;
            _inventoryController = SceneContext.Instance.InventoryController;

            _playerUIInput.OnFirePressed += FirePressedHandler;
        }

        private void FirePressedHandler()
        {
            if (_inventoryController.TryRemoveItem(_inventoryItem, COUNT_TO_REMOVE_AMMUNITION))
            {
                IDamageable lastDamageablesUnit = _lookTargetController.LastDamageablesUnit;
                lastDamageablesUnit?.TakeDamage(_damage);
            }
        }

        public void Dispose()
        {
            _playerUIInput.OnFirePressed -= FirePressedHandler;
        }
    }
}
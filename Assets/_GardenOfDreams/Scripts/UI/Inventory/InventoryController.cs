using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GardenOfDreams.Scripts.UI.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        private const byte INVENTORY_CELL_COUNT = 6;
        
        [SerializeField] private InventoryView _inventoryView;

        private InventoryModel _inventoryModel;
        private bool _isInventoryOpen = false;
        
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _inventoryView.Init(INVENTORY_CELL_COUNT);
            _inventoryModel = new InventoryModel(_inventoryView.InventoryItems);
            
            _inventoryView.ToggleInventory(false);
            
            _inventoryView.InventoryButton.onClick.AddListener(InventoryButtonHandler);
        }

        private void OnDestroy()
        {
            _inventoryView.InventoryButton.onClick.RemoveListener(InventoryButtonHandler);
        }

        private void InventoryButtonHandler()
        {
            _inventoryView.ToggleInventory(!_isInventoryOpen);
            _isInventoryOpen = !_isInventoryOpen;
        }

        [Button]
        public bool TryAddItem(InventoryItem inventoryItem, byte countToAdd)
        {
            if (_inventoryModel.TryAddItem(inventoryItem, countToAdd))
            {
                _inventoryView.UpdateItems(_inventoryModel.InventoryCells);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
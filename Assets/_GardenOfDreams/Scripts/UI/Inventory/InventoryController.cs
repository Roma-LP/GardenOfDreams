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

        public void Init()
        {
            _inventoryView.Init(INVENTORY_CELL_COUNT);
            _inventoryModel = new InventoryModel(_inventoryView.InventoryItems);

            _inventoryView.ToggleInventory(false);

            _inventoryView.InventoryButton.onClick.AddListener(InventoryButtonHandler);

            for (var i = 0; i < _inventoryView.InventoryItems.Count; i++)
            {
                _inventoryView.InventoryItems[i].OnCellClicked += CellButtonClicked;
                _inventoryView.InventoryItems[i].OnCloseClicked += CloseCellButtonClicked;
            }
        }

        private void OnDestroy()
        {
            _inventoryView.InventoryButton.onClick.RemoveListener(InventoryButtonHandler);

            for (var i = 0; i < _inventoryView.InventoryItems.Count; i++)
            {
                _inventoryView.InventoryItems[i].OnCellClicked -= CellButtonClicked;
                _inventoryView.InventoryItems[i].OnCloseClicked -= CloseCellButtonClicked;
            }
        }

        private void InventoryButtonHandler()
        {
            _inventoryView.ToggleInventory(!_isInventoryOpen);
            _isInventoryOpen = !_isInventoryOpen;
        }

        private void CellButtonClicked(InventoryCell inventoryCell)
        {
            if(inventoryCell.InventoryCellData == null)
                return;
            
            inventoryCell.SetCloseButtonView(true);
        }

        private void CloseCellButtonClicked(InventoryCell inventoryCell)
        {
            inventoryCell.DropItem();
            _inventoryModel.SaveData();
        }

        [Button]
        public bool TryAddItem(InventoryItem inventoryItem, byte countToAdd)
        {
            if (_inventoryModel.TryAddItem(inventoryItem, countToAdd))
            {
                _inventoryView.UpdateItems(_inventoryModel.InventoryCells);
                return true;
            }

            return false;
        }

        [Button]
        public bool TryRemoveItem(InventoryItem inventoryItem, byte countToRemove)
        {
            if (_inventoryModel.TryRemoveOneItem(inventoryItem, countToRemove))
            {
                _inventoryView.UpdateItems(_inventoryModel.InventoryCells);
                return true;
            }

            return false;
        }
    }
}
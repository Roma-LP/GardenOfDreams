using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _GardenOfDreams.Scripts.UI.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private InventoryCell _inventoryCellPrefab;
        [SerializeField] private Transform _parentContentForCells;
        [SerializeField] private List<InventoryCell> _inventoryItems = new List<InventoryCell>();
        
        public Button InventoryButton => _inventoryButton;
        public List<InventoryCell> InventoryItems => _inventoryItems;

        public void Init(byte _countCells)
        {
            for (int i = 0; i < _countCells; i++)
            {
                InventoryCell inventoryCell = Instantiate(_inventoryCellPrefab, _parentContentForCells);
                inventoryCell.ClearItem();
                _inventoryItems.Add(inventoryCell);
            }
        }

        public void ToggleInventory(bool show)
        {
            _inventoryPanel.SetActive(show);
        }

        public void UpdateItems(IReadOnlyList<InventoryCell> items)
        {
            for (var i = 0; i < _inventoryItems.Count; i++)
            {
               _inventoryItems[i].SetData(items[i].InventoryCellData);
            }
        }
    }
}

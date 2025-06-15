using System.Collections.Generic;
using _GardenOfDreams.Scripts.Utilities;
using UnityEngine;

namespace _GardenOfDreams.Scripts.UI.Inventory
{
    public class InventoryModel
    {
        private List<InventoryCell> _inventoryCells;
        private ItemDefinitionContainer _itemDefinitionContainer;
        
        public IReadOnlyList<InventoryCell> InventoryCells => _inventoryCells;

        public InventoryModel(List<InventoryCell> inventoryCells)
        {
            _inventoryCells = inventoryCells;
           
            _itemDefinitionContainer = SceneContext.Instance.ItemDefinitionContainer;
        }

        public bool TryAddItem(InventoryItem inventoryItem, byte countToAdd)
        {
            int remainderForAdd = countToAdd;
            ItemDefinition addItemDefinition = _itemDefinitionContainer.GetItemDefinitionByTypeItem(inventoryItem);

            for (int i = 0; i < _inventoryCells.Count; i++)
            {
                if (_inventoryCells[i].InventoryCellData == null)
                    continue;

                if (_inventoryCells[i].InventoryCellData.ItemDefinition.GetInventoryItem == inventoryItem)
                {
                    int AmountOfFreeSpaceInTheCell =
                        _inventoryCells[i].InventoryCellData.GetAmountOfFreeSpaceInTheCell();

                    if (AmountOfFreeSpaceInTheCell == 0)
                    {
                        continue;
                    }

                    if (AmountOfFreeSpaceInTheCell >= remainderForAdd)
                    {
                        _inventoryCells[i].InventoryCellData.AppendCount(remainderForAdd);
                        remainderForAdd = 0;
                    }
                    else
                    {
                        _inventoryCells[i].InventoryCellData.AppendCount(AmountOfFreeSpaceInTheCell);
                        remainderForAdd = countToAdd - AmountOfFreeSpaceInTheCell;
                    }
                }
            }

            if (remainderForAdd == 0)
                return true;


            for (int i = 0; i < _inventoryCells.Count && remainderForAdd > 0; i++)
            {
                if (_inventoryCells[i].InventoryCellData == null)
                {
                    int amountOfFreeSpaceInTheCell = addItemDefinition.GetMaxCountInStack;

                    if (amountOfFreeSpaceInTheCell >= remainderForAdd)
                    {
                        _inventoryCells[i].SetData(new InventoryCellData(addItemDefinition, remainderForAdd));
                        remainderForAdd = 0;
                    }
                    else
                    {
                        _inventoryCells[i].SetData(new InventoryCellData(addItemDefinition, amountOfFreeSpaceInTheCell));
                        remainderForAdd = remainderForAdd - amountOfFreeSpaceInTheCell;
                    }
                }
            }
            
            Debug.Log($"remainderForAdd:{remainderForAdd} inventoryItem:{inventoryItem}");

            if (remainderForAdd == countToAdd)
            {
                return false;
            }

            return true;
        }
    }
}
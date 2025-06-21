using System.Collections.Generic;
using _GardenOfDreams.Scripts.SaveTools.Models;
using _GardenOfDreams.Scripts.Utilities;

namespace _GardenOfDreams.Scripts.UI.Inventory
{
    public class InventoryModel
    {
        private List<InventoryCell> _inventoryCells;
        private ItemDefinitionContainer _itemDefinitionContainer;
        private InventoryData _inventoryData;

        public IReadOnlyList<InventoryCell> InventoryCells => _inventoryCells;

        public InventoryModel(List<InventoryCell> inventoryCells)
        {
            _inventoryCells = inventoryCells;

            _itemDefinitionContainer = SceneContext.Instance.ItemDefinitionContainer;
            _inventoryData = SceneContext.Instance.ProjectDatasContainer.InventoryData;

            LoadData();
        }

        private void LoadData()
        {
            if (_inventoryData.TryGetInventorySubData(out InventorySubData.InventoryCellSubData[] inventoryCellSubData))
            {
                for (int i = 0; i < _inventoryCells.Count; i++)
                {
                    if(inventoryCellSubData[i] == null)
                        continue;
                    
                    ItemDefinition addItemDefinition = _itemDefinitionContainer.GetItemDefinitionByTypeItem(inventoryCellSubData[i].InventoryItem);
                    _inventoryCells[i].SetData(new InventoryCellData(addItemDefinition, inventoryCellSubData[i].Count));
                }
            }
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
            {
                SaveData();
                return true;
            }

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
                        _inventoryCells[i]
                            .SetData(new InventoryCellData(addItemDefinition, amountOfFreeSpaceInTheCell));
                        remainderForAdd = remainderForAdd - amountOfFreeSpaceInTheCell;
                    }
                }
            }

            if (remainderForAdd == countToAdd)
            {
                return false;
            }

            SaveData();
            return true;
        }

        public bool TryRemoveOneItem(InventoryItem inventoryItem, int removeCount)
        {
            for (int i = 0; i < _inventoryCells.Count; i++)
            {
                if (_inventoryCells[i].InventoryCellData == null)
                    continue;

                if (_inventoryCells[i].InventoryCellData.ItemDefinition.GetInventoryItem == inventoryItem)
                {
                    _inventoryCells[i].InventoryCellData.RemoveItem(removeCount);
                    SaveData();
                    return true;
                }
            }

            return false;
        }

        public void SaveData()
        {
            _inventoryData.SaveInventory(InventoryCells);
        }
    }
}
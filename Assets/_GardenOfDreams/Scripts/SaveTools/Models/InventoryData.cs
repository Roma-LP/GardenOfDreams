using System;
using System.Collections.Generic;
using _GardenOfDreams.Scripts.UI.Inventory;

namespace _GardenOfDreams.Scripts.SaveTools.Models
{
    public class InventoryData : ProgressData<InventorySubData>
    {
        private InventorySubData _inventorySubData;

        private bool IsInventorySubDataNull()
        {
            return _inventorySubData == null;
        }

        public void SaveInventory(IReadOnlyList<InventoryCell> items)
        {
            if (IsInventorySubDataNull())
            {
                _inventorySubData = new InventorySubData()
                {
                    CountCells = items.Count,
                    InventoryCellSubDatas = new InventorySubData.InventoryCellSubData[items.Count + 1]
                };
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].InventoryCellData == null)
                {
                    _inventorySubData.InventoryCellSubDatas[i] = null;
                    continue;
                }
                
                _inventorySubData.InventoryCellSubDatas[i] = new InventorySubData.InventoryCellSubData()
                {
                    IndexCell = i,
                    InventoryItem = items[i].InventoryCellData.ItemDefinition.GetInventoryItem,
                    Count = items[i].InventoryCellData.Count
                };
            }
        }
        
        public bool TryGetInventorySubData(out InventorySubData.InventoryCellSubData[] inventoryCellSubDatas)
        {
            inventoryCellSubDatas = null;
            
            if (IsInventorySubDataNull())
            {
                return false;
            }

            inventoryCellSubDatas = _inventorySubData.InventoryCellSubDatas;
            return true;
        }

        public override InventorySubData GetProgressModel()
        {
            return _inventorySubData;
        }

        public override void SetProgressModel(InventorySubData state)
        {
            _inventorySubData = state;
        }
    }

    [Serializable]
    public class InventorySubData
    {
        public InventoryCellSubData[] InventoryCellSubDatas;
        public int CountCells;

        public class InventoryCellSubData
        {
            public int IndexCell;
            public InventoryItem InventoryItem;
            public int Count;
        }
    }
}
using UnityEngine;

namespace _GardenOfDreams.Scripts.UI.Inventory
{
    public class InventoryCellData
    {
        private ItemDefinition _itemDefinition;
        private int _count;

        public InventoryCellData(ItemDefinition itemDefinition, int count)
        {
            _itemDefinition = itemDefinition;
            _count = count;
        }

        public void AppendCount(int appendCount)
        {
            if (appendCount <= 0)
            {
                Debug.LogError($"[Inventory] Trying to add a negative number!\nAppendCount:{appendCount} Item: {_itemDefinition.GetInventoryItem}");
                return;
            }

            if (_count + appendCount > _itemDefinition.GetMaxCountInStack)
            {
                Debug.LogError($"[Inventory] Trying to overflow the stack of items in the inventory!\nCount:{_count + appendCount} CountInStack:{_itemDefinition.GetMaxCountInStack}");
                return;
            }
                
            _count = _count + appendCount;
        }

        public int GetAmountOfFreeSpaceInTheCell()
        {
            return _itemDefinition.GetMaxCountInStack - _count;
        }

        public ItemDefinition ItemDefinition => _itemDefinition;
        public int Count => _count;
    }
}
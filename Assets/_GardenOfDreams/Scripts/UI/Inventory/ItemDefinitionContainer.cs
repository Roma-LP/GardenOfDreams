using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _GardenOfDreams.Scripts.UI.Inventory
{
    [CreateAssetMenu(fileName = "NewInventoryDefinitionContainer", menuName = "Inventory/ItemDefinitionContainer")]
    public class ItemDefinitionContainer : ScriptableObject
    {
        [SerializeField] private List<ItemDefinition> _itemDefinitions;

        public ItemDefinition GetItemDefinitionByTypeItem(InventoryItem inventoryItem)
        {
            ItemDefinition definition = _itemDefinitions.FirstOrDefault(item => item.GetInventoryItem == inventoryItem);
            
            if (definition == null)
                Debug.LogError($"[Inventory] ItemDefinition for '{inventoryItem}' not found!");

            return definition;
        }
    }
}
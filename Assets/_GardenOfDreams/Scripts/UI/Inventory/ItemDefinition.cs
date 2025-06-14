using UnityEngine;

namespace _GardenOfDreams.Scripts.UI.Inventory
{
    [CreateAssetMenu(fileName = "NewInventoryDefinition", menuName = "Inventory/ItemDefinition")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _maxCountInStack;

        public Sprite GetSprite => _sprite;
        public int GetMaxCountInStack => _maxCountInStack;
        public InventoryItem GetInventoryItem => _inventoryItem;
    }
}
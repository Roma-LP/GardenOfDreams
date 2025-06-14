using _GardenOfDreams.Scripts.UI.Inventory;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Utilities
{
    public class SceneContext : Singleton<SceneContext>
    {
        //[SerializeField] private Player _player;
        [SerializeField] private ItemDefinitionContainer _itemDefinitionContainer;
        [SerializeField] private InventoryController _inventoryController;

        public ItemDefinitionContainer ItemDefinitionContainer => _itemDefinitionContainer;
        public InventoryController InventoryController => _inventoryController;

        protected override void Awake()
        {
            base.Awake();
            
            Bootstrapper();
        }
        
        private void Bootstrapper()
        {
            Application.targetFrameRate = 60;
        
            
        }
    }
}
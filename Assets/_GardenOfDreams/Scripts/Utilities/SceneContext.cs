using _GardenOfDreams.Scripts.UI.HP;
using _GardenOfDreams.Scripts.UI.Inventory;
using _GardenOfDreams.Scripts.UI.PlayerInput;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Utilities
{
    public class SceneContext : Singleton<SceneContext>
    {
        //[SerializeField] private Player _player;
        [SerializeField] private ItemDefinitionContainer _itemDefinitionContainer;
        [SerializeField] private InventoryController _inventoryController;
        [SerializeField] private WorldHpBarSpawner _worldHpBarSpawner;
        [SerializeField] private PlayerUIInput _playerUIInput;

        public ItemDefinitionContainer ItemDefinitionContainer => _itemDefinitionContainer;
        public InventoryController InventoryController => _inventoryController;
        public WorldHpBarSpawner WorldHpBarSpawner => _worldHpBarSpawner;
        public PlayerUIInput PlayerUIInput => _playerUIInput;

        protected override void Awake()
        {
            base.Awake();
            
            Bootstrapper();
        }
        
        private void Bootstrapper()
        {
            Application.targetFrameRate = 60;
        
            _inventoryController.Init();
        }
    }
}
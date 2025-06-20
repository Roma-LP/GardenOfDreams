using _GardenOfDreams.Scripts.Player;
using _GardenOfDreams.Scripts.SaveTools;
using _GardenOfDreams.Scripts.Spawners;
using _GardenOfDreams.Scripts.UI.HP;
using _GardenOfDreams.Scripts.UI.Inventory;
using _GardenOfDreams.Scripts.UI.PlayerInput;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Utilities
{
    public class SceneContext : Singleton<SceneContext>
    {
        [SerializeField] private ItemDefinitionContainer _itemDefinitionContainer;
        [SerializeField] private InventoryController _inventoryController;
        [SerializeField] private WorldHpBarSpawner _worldHpBarSpawner;
        [SerializeField] private PlayerUIInput _playerUIInput;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PlayerSpawner _playerSpawner;

        private ProjectDatasContainer _projectDatasContainer;
        private PlayerUnit _playerUnit;
        
        public PlayerUnit PlayerUnit => _playerUnit;
        public ItemDefinitionContainer ItemDefinitionContainer => _itemDefinitionContainer;
        public InventoryController InventoryController => _inventoryController;
        public WorldHpBarSpawner WorldHpBarSpawner => _worldHpBarSpawner;
        public PlayerUIInput PlayerUIInput => _playerUIInput;
        public ProjectDatasContainer ProjectDatasContainer => _projectDatasContainer;

        protected override void Awake()
        {
            base.Awake();
            
            Bootstrapper();
        }
        
        private void Bootstrapper()
        {
            Application.targetFrameRate = 60;

            _projectDatasContainer = new ProjectDatasContainer();
        
            _projectDatasContainer.Init();
            _playerSpawner.Init(out _playerUnit);
            _inventoryController.Init();
            _enemySpawner.Init();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _projectDatasContainer.Dispose();
        }
    }
}
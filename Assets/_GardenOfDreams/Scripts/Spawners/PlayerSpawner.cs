using _GardenOfDreams.Scripts.Player;
using _GardenOfDreams.Scripts.SaveTools.Models;
using _GardenOfDreams.Scripts.Utilities;
using Cinemachine;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerUnit _playerUnit;
        [SerializeField] private Transform _defaultSpawnPoint;
        [SerializeField] private CinemachineVirtualCamera _cmVirtualCamera;

        public void Init(out PlayerUnit playerUnit)
        {
            PlayerData playerData = SceneContext.Instance.ProjectDatasContainer.PlayerData;

            if (playerData.TryGetPosition(out Vector3 position))
            {
                playerUnit = InstantiatePlayer(position);
            }
            else
            {
                playerUnit = InstantiatePlayer(_defaultSpawnPoint.position);
            }
            
            _cmVirtualCamera.Follow = playerUnit.transform;
        }

        private PlayerUnit InstantiatePlayer(Vector3 position)
        {
           return Instantiate(_playerUnit, position, Quaternion.identity);
        }
    }
}
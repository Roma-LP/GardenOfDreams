using System.Collections.Generic;
using _GardenOfDreams.Scripts.Interfaces;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Spawners
{
    public abstract class SpawnerBase<T,P> : MonoBehaviour where T : MonoBehaviour, ISpawnable<P>
    {
        [SerializeField] private T _spawnObject;
        [SerializeField] private Transform _container;
        [SerializeField, Range(1, 10)] private int _spawnCount = 3;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private bool _isUseRandomSpawnPoints;

        public abstract void Init();

        protected void SpawnFromConfig(P paramsFowSpawned)
        {
            if (_spawnObject == null)
            {
                Debug.LogError($"SpawnObject in {nameof(T)}");
                return;
            }

            for (int i = 0; i < _spawnCount; i++)
            {
                Transform spawnPoint = _isUseRandomSpawnPoints
                    ? _spawnPoints[Random.Range(0, _spawnPoints.Count)]
                    : _spawnPoints[i % _spawnPoints.Count];

                T instance = Instantiate(_spawnObject, spawnPoint.position, Quaternion.identity, _container);
                instance.OnSpawned(paramsFowSpawned);
            }
        }
    }
}
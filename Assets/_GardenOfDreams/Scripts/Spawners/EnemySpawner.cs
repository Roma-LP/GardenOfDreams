using _GardenOfDreams.Scripts.Zombie;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Spawners
{
    public class EnemySpawner : SpawnerBase<ZombieUnit, ZombieLinks>
    {
       [SerializeField] private ZombieLinks _zombieLinks;

       public override void Init()
       {
           SpawnFromConfig(_zombieLinks);
       }
    }
}

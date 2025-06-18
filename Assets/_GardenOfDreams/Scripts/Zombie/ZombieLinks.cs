using System.Collections.Generic;
using UnityEngine;
using System;

namespace _GardenOfDreams.Scripts.Zombie
{
    [Serializable]
    public class ZombieLinks
    {
       [SerializeField] private List<Transform> _pointsToPatrol;
       [SerializeField] private DropItem _dropItemWhenDies;

        public List<Transform> PointsToPatrol => _pointsToPatrol;
        public DropItem DropItemWhenDies => _dropItemWhenDies;
    }
}

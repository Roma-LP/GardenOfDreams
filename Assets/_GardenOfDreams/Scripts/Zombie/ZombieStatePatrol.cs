using System.Collections.Generic;
using _GardenOfDreams.Scripts.StateMachineStuff;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Zombie
{
    public class ZombieStatePatrol : FSMState
    {
        [SerializeField] private List<Transform> _pointsToPatrol;
        [SerializeField] private Zombie _zombie;
        [SerializeField] private float _waitTime = 5f;
        [SerializeField] private float _remainingDistanceToStop = 0.02f;
        
        private Transform _currentPoint;
        private float _waitCounter;
        private bool _isWaiting = false;
        private int _currentPatrolIndex;
        
        public override void Enter()
        {
            GoToNextPoint();
        }

        public override void UpdateState()
        {
            if (_isWaiting)
            {
                _waitCounter -= Time.deltaTime;
                if (_waitCounter <= 0f)
                {
                    GoToNextPoint();
                }
            }
            else
            {
                if (!_zombie.Agent.pathPending && _zombie.Agent.remainingDistance <= _remainingDistanceToStop)
                {
                    StartWaiting();
                }
            }
        }

        private void GoToNextPoint()
        {
            _isWaiting = false;

            if (_pointsToPatrol.Count == 0) return;

            _currentPatrolIndex = (_currentPatrolIndex + 1) % _pointsToPatrol.Count;
            _currentPoint = _pointsToPatrol[_currentPatrolIndex];
            _zombie.MoveTo(_currentPoint.position);
        }
        
        private void StartWaiting()
        {
            _waitCounter = _waitTime;
            _zombie.Agent.ResetPath();
            _zombie.StopMoving();
            _isWaiting = true;
        }
    }
}
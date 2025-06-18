using System.Collections.Generic;
using _GardenOfDreams.Scripts.StateMachineStuff;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Zombie.FSM
{
    public class ZombieStatePatrol : FSMState
    {
        [SerializeField] private List<Transform> _pointsToPatrol;
        [SerializeField] private ZombieUnit _zombieUnit;
        [SerializeField] private bool _useRandomPatrol = true;
        [SerializeField] private float _waitTime = 5f;
        [SerializeField] private float _remainingDistanceToStop = 0.02f;
        
        private Transform _currentPoint;
        private float _waitCounter;
        private bool _isWaiting = false;
        private int _currentPatrolIndex;

        private void OnEnable()
        {
            SetLinks();
            GoToNextPoint();
        }

        private void SetLinks()
        {
            if (_pointsToPatrol.Count != 0)
                return;
            
            _pointsToPatrol = _zombieUnit.ZombieLinks.PointsToPatrol;
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
                if (!_zombieUnit.Agent.pathPending && _zombieUnit.Agent.remainingDistance <= _remainingDistanceToStop)
                {
                    StartWaiting();
                }
            }
        }

        private void GoToNextPoint()
        {
            _isWaiting = false;

            if (_pointsToPatrol.Count == 0)
                return;

            if (_useRandomPatrol)
            {
                int nextIndex;
                do
                {
                    nextIndex = Random.Range(0, _pointsToPatrol.Count);
                } while (_pointsToPatrol.Count > 1 && nextIndex == _currentPatrolIndex);

                _currentPatrolIndex = nextIndex;
            }
            else
            {
                _currentPatrolIndex = (_currentPatrolIndex + 1) % _pointsToPatrol.Count;
            }
            
            _currentPoint = _pointsToPatrol[_currentPatrolIndex];
            _zombieUnit.MoveTo(_currentPoint.position);
        }
        
        private void StartWaiting()
        {
            _waitCounter = _waitTime;
            _zombieUnit.Agent.ResetPath();
            _zombieUnit.StopMoving();
            _isWaiting = true;
        }
    }
}
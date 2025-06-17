using System.Collections;
using _GardenOfDreams.Scripts.Player;
using _GardenOfDreams.Scripts.StateMachineStuff;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Zombie.FSM
{
    public class ZombieTransitionAttack : FSMTransition
    {
        private enum TriggerMode
        {
            PlayerInsideRadius,
            PlayerOutsideRadius
        }
        
        [SerializeField] private ZombieUnit _zombieUnit;
        [SerializeField] private float _activationRadius = 2f;
        [SerializeField] private TriggerMode _triggerMode = TriggerMode.PlayerInsideRadius;
        [SerializeField] private float _checkInterval = 0.2f;

        private PlayerUnit _playerUnit;
        private Coroutine _checkRoutine;
        private WaitForSeconds _waitForSeconds;
        
        public override void Init()
        {
            base.Init();

            _playerUnit = _zombieUnit.PlayerUnit;
            _waitForSeconds = new WaitForSeconds(_checkInterval);
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();

            _checkRoutine = StartCoroutine(CheckDistanceRoutine());
        }
        
        private void OnDisable()
        {
            if (_checkRoutine != null)
            {
                StopCoroutine(_checkRoutine);
            }
        }
        
        private IEnumerator CheckDistanceRoutine()
        {
            while (true)
            {
                yield return _waitForSeconds;

                if (_playerUnit is null)
                    continue;

                float distance = Vector3.Distance(transform.position, _playerUnit.transform.position);
                if (Compare(distance, _activationRadius, _triggerMode))
                {
                    NeedTransit = true;
                }
            }
        }

        private bool Compare(float valueA, float valueB, TriggerMode comparison)
        {
            switch (comparison)
            {
                case TriggerMode.PlayerInsideRadius:
                    return valueA <= valueB;
                case TriggerMode.PlayerOutsideRadius:
                    return valueA >= valueB;
                default:
                    return false;
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            if (_playerUnit != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, _activationRadius);
            }
        }
    }
}
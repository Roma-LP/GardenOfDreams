using UnityEngine;

namespace _GardenOfDreams.Scripts.StateMachineStuff
{
    public class FSMTransition : MonoBehaviour
    {
        [SerializeField] private FSMState _targetState;
        
        public FSMState TargetState => _targetState;
        public bool NeedTransit { get; protected set; }

        public void Init()
        {
        }

        private void OnEnable()
        {
            NeedTransit = false;
        }
    }
}
using UnityEngine;

namespace _GardenOfDreams.Scripts.StateMachineStuff
{
    public class FSMTransition : MonoBehaviour
    {
        [SerializeField] private FSMState _targetState;
        
        public FSMState TargetState => _targetState;
        public bool NeedTransit { get; protected set; }

        public virtual void Init()
        {
        }

        protected virtual void OnEnable()
        {
            NeedTransit = false;
        }
    }
}
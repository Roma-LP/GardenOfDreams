using System;
using UnityEngine;
using UnityEngine.UI;

namespace _GardenOfDreams.Scripts.UI.PlayerInput
{
    public class PlayerUIInput : MonoBehaviour
    {
        [SerializeField] private Button _fireButton;
        [SerializeField] private Joystick _joystick;

        public Joystick Joystick => _joystick;

        public event Action OnFirePressed;

        private void Awake()
        {
            _fireButton.onClick.AddListener(InvokeFire);
        }

        private void OnDestroy()
        {
            _fireButton.onClick.RemoveListener(InvokeFire);
        }

        private void InvokeFire()
        {
            OnFirePressed?.Invoke();
        }
    }
}
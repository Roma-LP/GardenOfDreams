using System;
using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.UI.PlayerInput;
using _GardenOfDreams.Scripts.Utilities;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class PlayerUIInputHandler : IDisposable
    {
        private PlayerUIInput _playerUIInput;
        private LookTargetController _lookTargetController;
        private float _damage;

        public PlayerUIInputHandler(LookTargetController lookTargetController, float damage)
        {
            _playerUIInput = SceneContext.Instance.PlayerUIInput;
            _lookTargetController = lookTargetController;
            _damage = damage;

            _playerUIInput.OnFirePressed += FirePressedHandler;
        }

        private void FirePressedHandler()
        {
            IDamageable lastDamageablesUnit = _lookTargetController.LastDamageablesUnit;

            lastDamageablesUnit?.TakeDamage(_damage);
        }

        public void Dispose()
        {
            _playerUIInput.OnFirePressed -= FirePressedHandler;
        }
    }
}
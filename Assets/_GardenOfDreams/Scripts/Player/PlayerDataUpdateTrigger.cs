using System;
using System.Collections;
using _GardenOfDreams.Scripts.SaveTools.Models;
using _GardenOfDreams.Scripts.Utilities;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class PlayerDataUpdateTrigger : IDisposable
    {
        private const int INTERVAL = 2;
        
        private readonly Coroutine _checkRoutine;
        private readonly WaitForSeconds _waitForSeconds;
        private readonly PlayerData _playerData;
        private readonly PlayerUnit _playerUnit;
        
        public PlayerDataUpdateTrigger(PlayerUnit playerUnit)
        {
            _waitForSeconds = new WaitForSeconds(INTERVAL);
            _playerData = SceneContext.Instance.ProjectDatasContainer.PlayerData;
            _playerUnit = playerUnit;

            _playerUnit.OnHealthChanged += HandleHealthChanged;
            
            _checkRoutine = _playerUnit.StartCoroutine(PeriodDataUpdate());
        }

        private IEnumerator PeriodDataUpdate()
        {
            while (true)
            {
                yield return _waitForSeconds;
                _playerData.SavePosition(_playerUnit.transform.position);
            }
        }
        
        private void HandleHealthChanged(float current, float max)
        {
            _playerData.SaveHealth(current);
        }

        public void Dispose()
        {
            _playerUnit.OnHealthChanged -= HandleHealthChanged;
            
            if (_checkRoutine != null)
            {
                _playerUnit.StopCoroutine(_checkRoutine);
            }
        }
    }
}
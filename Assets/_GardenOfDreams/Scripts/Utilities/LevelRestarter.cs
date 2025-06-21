using System;
using _GardenOfDreams.Scripts.Player;
using UnityEngine.SceneManagement;

namespace _GardenOfDreams.Scripts.Utilities
{
    public class LevelRestarter : IDisposable
    {
        private PlayerUnit _playerUnit;
        
        public LevelRestarter(PlayerUnit playerUnit)
        {
            _playerUnit = playerUnit;

            _playerUnit.OnHealthChanged += HandleHealthChanged;
        }

        private void HandleHealthChanged(float current, float max)
        {
            if (current <= 0)
            {
                SceneContext.Instance.ProjectDatasContainer.ClearSaveFile();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void Dispose()
        {
            _playerUnit.OnHealthChanged -= HandleHealthChanged;
        }
    }
}
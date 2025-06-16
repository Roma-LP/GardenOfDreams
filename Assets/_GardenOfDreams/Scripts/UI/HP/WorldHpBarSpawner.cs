using UnityEngine;

namespace _GardenOfDreams.Scripts.UI.HP
{
    public class WorldHpBarSpawner : MonoBehaviour
    {
        [SerializeField] private HealthBarUI _hpBarPrefab;
        [SerializeField] private Transform _healthBarContainer;

        public HealthBarUI CreateHpBar(float maxHealth)
        {
            HealthBarUI barUI = Instantiate(_hpBarPrefab, _healthBarContainer.transform);

            barUI.Init(maxHealth);

            return barUI;
        }
    }
}

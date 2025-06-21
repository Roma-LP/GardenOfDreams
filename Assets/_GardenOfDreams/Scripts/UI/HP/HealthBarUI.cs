using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _GardenOfDreams.Scripts.UI.HP
{
    public class HealthBarUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fillImage;
        [SerializeField] private RectTransform _rectToMove;

        [Header("Colors")]
        [SerializeField] private Color _fullHealthColor = Color.green;
        [SerializeField] private Color _lowHealthColor = Color.red;

        [Header("Animation")]
        [SerializeField] private float _updateDuration = 0.3f;

        private float _maxHealth;
        
        public RectTransform RectTransformToMove => _rectToMove;

        public void Init(float currentHealth, float maxHealth)
        {
            _maxHealth = maxHealth;
            _slider.maxValue = maxHealth;
            _slider.value = currentHealth;
            UpdateColor(1f, 0);
        }

        public void SetHealth(float currentHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, _maxHealth);
            float normalized = currentHealth / _maxHealth;
            
            _slider.DOValue(currentHealth, _updateDuration).SetEase(Ease.OutQuad).SetLink(gameObject);
            
            UpdateColor(normalized, _updateDuration);
        }

        private void UpdateColor(float normalizedValue, float updateDuration)
        {
            Color targetColor = Color.Lerp(_lowHealthColor, _fullHealthColor, normalizedValue);
            _fillImage.DOColor(targetColor, updateDuration).SetEase(Ease.OutQuad).SetLink(gameObject);;
        }
    }
}
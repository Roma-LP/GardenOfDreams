using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.UI.Inventory;
using DG.Tweening;
using UnityEngine;

namespace _GardenOfDreams.Scripts
{
    public class DropItem : MonoBehaviour, IPickable
    {
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField, Range(1,50)] private byte _countItem;
        [Header("Pulse Settings")]
        [SerializeField] private float _pulseScale = 1.2f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Transform _targetTransform;

        private Tween _pulseTween;
        private Vector3 _baseScale;
        
        public InventoryItem InventoryItem => _inventoryItem;
        public byte CountItem => _countItem;

        private void Start()
        {
            StartPulse();
        }

        public void PickedUp()
        {
            StopPulse();
            Destroy(gameObject);
        }
        
        private void StartPulse()
        {
            _baseScale = _targetTransform.localScale;

            _pulseTween = _targetTransform
                .DOScale(_pulseScale, _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .SetLink(gameObject);
        }

        private void StopPulse()
        {
            if (_pulseTween != null && _pulseTween.IsActive())
            {
                _pulseTween.Kill();
                _targetTransform.localScale = Vector3.one;
            }
        }
    }
}

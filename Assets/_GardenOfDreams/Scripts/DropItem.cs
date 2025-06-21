using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.SaveTools.Models;
using _GardenOfDreams.Scripts.UI.Inventory;
using _GardenOfDreams.Scripts.Utilities;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GardenOfDreams.Scripts
{
    public class DropItem : MonoBehaviour, IPickable
    {
        [SerializeField, ShowIf(nameof(_isNeedToSave))] private int _id;
        [SerializeField] private bool _isNeedToSave;
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField, Range(1,50)] private byte _countItem;
        [Header("Pulse Settings")]
        [SerializeField] private float _pulseScale = 1.2f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Transform _targetTransform;

        private Tween _pulseTween;
        private Vector3 _baseScale;
        private DropItemData _dropItemData;
        
        public InventoryItem InventoryItem => _inventoryItem;
        public byte CountItem => _countItem;

        private void Start()
        {
            _dropItemData = SceneContext.Instance.ProjectDatasContainer.DropItemData;

            if (_isNeedToSave && _dropItemData.IsPickupItem(_id))
            {
                Destroy(gameObject);
            }
            
            StartPulse();
        }

        public void PickedUp()
        {
            StopPulse();
            _dropItemData.PickupItem(_id);
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

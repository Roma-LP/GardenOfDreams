using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GardenOfDreams.Scripts.UI.Inventory
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _maxCount;
        [SerializeField] private Button _buttonDropItem;

        private bool _isFree;
        private InventoryCellData _inventoryCellData;

        public InventoryCellData InventoryCellData => _inventoryCellData;
        
        public event Action OnCloseClicked;

        private void OnEnable()
        {
            _buttonDropItem.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            _buttonDropItem.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            OnCloseClicked?.Invoke();
        }

        public void SetData(InventoryCellData inventoryCellData)
        {
            _inventoryCellData = inventoryCellData;
            
            _image.sprite = _inventoryCellData.ItemDefinition.GetSprite;
            _count.text =  _inventoryCellData.Count.ToString();
            _maxCount.text = _inventoryCellData.ItemDefinition.GetMaxCountInStack.ToString();
            
            _image.gameObject.SetActive(true);
            _buttonDropItem.gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            _inventoryCellData = null;
            
            _image.gameObject.SetActive(false);
            _count.text = "";
            _maxCount.text = "";
            _buttonDropItem.gameObject.SetActive(false);
        }

        public void DropItem()
        {
            ClearItem();

            OnCloseClicked?.Invoke();
        }
    }
}
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
        [SerializeField] private GameObject _texts;

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
            DropItem();
            OnCloseClicked?.Invoke();
        }

        private void SetTextView(bool active)
        {
            _texts.SetActive(active);
        }

        public void SetData(InventoryCellData inventoryCellData)
        {
            if (inventoryCellData == null)
                return;

            _inventoryCellData = inventoryCellData;
            
            _image.sprite = _inventoryCellData.ItemDefinition.GetSprite;
            _count.text =  _inventoryCellData.Count.ToString();
            _maxCount.text = _inventoryCellData.ItemDefinition.GetMaxCountInStack.ToString();
            
            _image.gameObject.SetActive(true);
            _buttonDropItem.gameObject.SetActive(true);

            if (inventoryCellData.Count == 0)
            {
                ClearItem();
            }

            SetTextView(inventoryCellData.Count != 1);
        }

        public void ClearItem()
        {
            _inventoryCellData = null;
            
            _image.gameObject.SetActive(false);
            _count.text = "";
            _maxCount.text = "";
            _buttonDropItem.gameObject.SetActive(false);
            SetTextView(false);
        }

        public void DropItem()
        {
            ClearItem();
        }
    }
}
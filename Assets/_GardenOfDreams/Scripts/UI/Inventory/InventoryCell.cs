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
        [SerializeField] private Button _buttonCellClick;
        [SerializeField] private GameObject _texts;

        private InventoryCellData _inventoryCellData;

        public InventoryCellData InventoryCellData => _inventoryCellData;
        
        public event Action<InventoryCell> OnCloseClicked;
        public event Action<InventoryCell> OnCellClicked;

        private void OnEnable()
        {
            _buttonDropItem.onClick.AddListener(HandleDropItemClick);
            _buttonCellClick.onClick.AddListener(HandleCellClick);
        }

        private void OnDisable()
        {
            _buttonDropItem.onClick.RemoveListener(HandleDropItemClick);
            _buttonCellClick.onClick.RemoveListener(HandleCellClick);

            SetCloseButtonView(false);
        }

        private void HandleDropItemClick()
        {
            OnCloseClicked?.Invoke(this);
        }

        private void HandleCellClick()
        {
            OnCellClicked?.Invoke(this);
        }

        private void SetTextView(bool active)
        {
            _texts.SetActive(active);
        }

        public void SetCloseButtonView(bool active)
        {
            _buttonDropItem.gameObject.SetActive(active);
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

            if (inventoryCellData.Count == 0)
            {
                ClearItem();
                return;
            }

            SetTextView(inventoryCellData.Count != 1);
        }

        public void ClearItem()
        {
            _inventoryCellData = null;
            
            _image.gameObject.SetActive(false);
            _count.text = "";
            _maxCount.text = "";
            SetCloseButtonView(false);
            SetTextView(false);
        }

        public void DropItem()
        {
            ClearItem();
        }
    }
}
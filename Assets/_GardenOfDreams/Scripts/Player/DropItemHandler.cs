using System;
using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.UI.Inventory;
using _GardenOfDreams.Scripts.Utilities;
using UnityEngine;

namespace _GardenOfDreams.Scripts.Player
{
    public class DropItemHandler : MonoBehaviour
    {
        [SerializeField] private DropItemDetector _dropItemDetector;

        private InventoryController _inventoryController;

        public void Init()
        {
            _inventoryController = SceneContext.Instance.InventoryController;

            _dropItemDetector.OnDropItemEntered += TryPickItem;
        }

        private void OnDestroy()
        {
            _dropItemDetector.OnDropItemEntered -= TryPickItem;
        }

        private void TryPickItem(IPickable iPickable)
        {
            if (_inventoryController.TryAddItem(iPickable.InventoryItem, iPickable.CountItem))
            {
                iPickable.PickedUp();
            }
        }
    }
}
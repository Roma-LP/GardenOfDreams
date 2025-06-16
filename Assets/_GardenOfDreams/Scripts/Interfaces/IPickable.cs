using _GardenOfDreams.Scripts.UI.Inventory;

namespace _GardenOfDreams.Scripts.Interfaces
{
    public interface IPickable
    {
        void PickedUp();
        InventoryItem InventoryItem { get; }
        byte CountItem { get; }
    }
}
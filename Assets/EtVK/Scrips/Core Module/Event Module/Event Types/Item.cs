using EtVK.Player_Module.Interactable;

namespace EtVK.Event_Module.Event_Types
{
    [System.Serializable] public struct Item
    {
        public Inventory_Module.Item InteractItem { get; set; }
        public Interactable Interactable { get; set; }

        public Item(Inventory_Module.Item interactItem, Interactable interactable)
        {
            InteractItem = interactItem;
            Interactable = interactable;
        }
    }
}
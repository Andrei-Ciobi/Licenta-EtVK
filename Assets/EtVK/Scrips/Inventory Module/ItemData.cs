using EtVK.Scrips.Utyles;
using UnityEngine;

namespace EtVK.Scrips.Inventory_Module
{
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private Sprite itemIcon;
        [SerializeField] private ItemType itemType;
        [SerializeField] private GameObject prefab;
        [SerializeField] private string itemName;
        [SerializeField] private string itemDescription;


        public Sprite ItemIcon => itemIcon;

        public ItemType ItemType => itemType;

        public string ItemName => itemName;

        public GameObject Prefab => prefab;

        public string ItemDescription => itemDescription;
    }
}
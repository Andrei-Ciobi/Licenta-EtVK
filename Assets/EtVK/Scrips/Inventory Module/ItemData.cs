using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] protected Sprite itemIcon;
        [SerializeField] protected ItemType itemType;
        [SerializeField] protected GameObject prefab;
        [SerializeField] protected string itemName;
        [SerializeField] protected string itemDescription;
        [SerializeField] protected bool isEquipped;


        public Sprite ItemIcon => itemIcon;

        public ItemType ItemType => itemType;

        public string ItemName => itemName;

        public GameObject Prefab => prefab;

        public string ItemDescription => itemDescription;

        public bool IsEquipped => isEquipped;
    }
}
using System.Collections.Generic;
using EtVK.Items_Module.Armors.BackAttachment;
using EtVK.Items_Module.Armors.Chestplate;
using EtVK.Items_Module.Armors.Helmet;
using EtVK.Items_Module.Armors.Pants;
using EtVK.Items_Module.Armors.UpperBracers;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/Armor/ArmorSet", order = 0)]
    public class ArmorSetData : ScriptableObject
    {
        [SerializeField] private HelmetData helmet;
        [SerializeField] private ChestplateData chestPlate;
        [SerializeField] private BackAttachmentData backAttachment;
        [SerializeField] private PantsData pants;
        [SerializeField] private UpperBracersData upperBracers;

        public HelmetData Helmet => helmet;
        public ChestplateData ChestPlate => chestPlate;
        public BackAttachmentData BackAttachment => backAttachment;
        public PantsData Pants => pants;
        public UpperBracersData UpperBracers => upperBracers;

        public List<ItemData> GetArmorSet()
        {
            var armor = new List<ItemData> {helmet, chestPlate, backAttachment, pants, upperBracers};
            armor.RemoveAll(x => x == null);

            return armor;
        }
    }
}
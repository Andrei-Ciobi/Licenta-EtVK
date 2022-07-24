using System;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Armors.BackAttachment
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/Armor/NewBackAttachment")]
    public class BackAttachmentData : ArmorData
    {
        private void Awake()
        {
            armorType = ArmorType.Back;
        }
    }
}
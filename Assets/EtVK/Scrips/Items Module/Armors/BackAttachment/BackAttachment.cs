using UnityEngine;

namespace EtVK.Items_Module.Armors.BackAttachment
{
    public class BackAttachment : Armor
    {
        [SerializeField] private BackAttachmentData backAttachmentData;
        
        private void Awake()
        {
            armorData = backAttachmentData;
            InitializeReferences();
        }
    }
}
using EtVK.Inventory_Module;
using EtVK.Inventory_Module.Holder_Slots;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Armors.Helmet
{
    public class Helmet : Armor
    {
        [SerializeField] private HelmetData helmetData;
        [SerializeField] private SerializableSet<MeshFilter, SkinnedMeshRenderer> helmetMesh;
        [SerializeField] private SerializableSet<MeshFilter, SkinnedMeshRenderer> attachmentMesh;

        private void Awake()
        {
            armorData = helmetData;
            InitializeReferences();
        }
        
        protected override void DeactivateVisual()
        {
            helmetMesh.GetValue().gameObject.SetActive(true);
            attachmentMesh.GetValue()?.gameObject.SetActive(true);
            
            helmetMesh.GetKey().gameObject.SetActive(false);
            attachmentMesh.GetKey()?.gameObject.SetActive(false);
        }

        protected override void SetSkinBones(ArmorHolderSlot armorHolderSlot)
        {
            var helmetSlot = (HelmetHolderSlot) armorHolderSlot;
            var defaultDualSlot = armorHolderSlot.GetComponentInChildren<DefaultDualSlot>();
            helmetMesh.GetValue().bones = defaultDualSlot.LeftMesh.bones;
            helmetMesh.GetValue().rootBone = defaultDualSlot.LeftMesh.rootBone;

            if (attachmentMesh.GetValue() != null)
            {
                attachmentMesh.GetValue().bones = defaultDualSlot.RightMesh.bones;
                attachmentMesh.GetValue().rootBone = defaultDualSlot.RightMesh.rootBone;
            }
            
            helmetSlot.DependencyList.ForEach(x => x.SetActive(false));
        }
    }
}
using System;
using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Armors.UpperBracers
{
    public class UpperBracers : Armor
    {
        [SerializeField] private UpperBracersData upperBracersData;
        [SerializeField] private SerializableSet<MeshFilter, SkinnedMeshRenderer> leftMesh;
        [SerializeField] private SerializableSet<MeshFilter, SkinnedMeshRenderer> rightMesh;

        private void Awake()
        {
            armorData = upperBracersData;
        }

        protected override void DeactivateVisual()
        {
            leftMesh.GetValue().gameObject.SetActive(true);
            rightMesh.GetValue().gameObject.SetActive(true);
            
            leftMesh.GetKey().gameObject.SetActive(false);
            rightMesh.GetKey().gameObject.SetActive(false);
        }

        protected override void SetSkinBones(ArmorHolderSlot armorHolderSlot)
        {
            var defaultDualSlot = armorHolderSlot.GetComponentInChildren<DefaultDualSlot>();
            leftMesh.GetValue().bones = defaultDualSlot.LeftMesh.bones;
            leftMesh.GetValue().rootBone = defaultDualSlot.LeftMesh.rootBone;

            rightMesh.GetValue().bones = defaultDualSlot.RightMesh.bones;
            rightMesh.GetValue().rootBone = defaultDualSlot.RightMesh.rootBone;
            
            defaultDualSlot.gameObject.SetActive(false);
        }
    }
}
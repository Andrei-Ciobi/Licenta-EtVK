using EtVK.Core.Manager;
using UnityEngine;

namespace EtVK.Items_Module.Off_Hand.Shield
{
    public class Shield : OffHand
    {
        [SerializeField] private ShieldData shieldData;

        private void Awake()
        {
            offHandData = shieldData;
        }

        public override void DrawOffHand()
        {
            transform.parent = currentHolderSlot.DrawParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isArmed = true;

            var blockingManager = transform.root.gameObject.GetComponentInChildren<BlockingManager>();
            if(blockingManager == null)
                return;
            
            blockingManager.SetAbsorption(shieldData.Absorption);
        }

        public override void WithdrawOffHand()
        {
            transform.parent = currentHolderSlot.ParentOverride;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isArmed = false;
            
            var blockingManager = transform.root.gameObject.GetComponentInChildren<BlockingManager>();
            if(blockingManager == null)
                return;
            
            blockingManager.SetAbsorption(0);
        }
        
        
    }
}
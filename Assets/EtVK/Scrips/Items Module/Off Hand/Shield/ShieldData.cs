using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Off_Hand.Shield
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/OffHand/ShieldData")]
    public class ShieldData : OffHandData
    {
        [SerializeField][Range(0, 100)] private int absorption;
        
        public int Absorption => absorption;

        private void Awake()
        {
            offHandType = OffHandType.Shield;
        }
    }
}
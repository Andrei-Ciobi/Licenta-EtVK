using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Items_Module.Off_Hand.Shield
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/OffHand/ShieldData")]
    public class ShieldData : OffHandData
    {
        private void Awake()
        {
            offHandType = OffHandType.Shield;
        }
    }
}
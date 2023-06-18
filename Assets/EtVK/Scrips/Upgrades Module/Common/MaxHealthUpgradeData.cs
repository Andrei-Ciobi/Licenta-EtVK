using EtVK.Player_Module.Manager;
using EtVK.Upgrades_Module.Core;
using UnityEngine;

namespace EtVK.Upgrades_Module.Common
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Upgrades/Common/MaxHealth")]
    public class MaxHealthUpgradeData : CommonUpgradeData
    {
        private void Awake()
        {
            description = $"+{variableName} more max health";
        }
        public override void Action(PlayerManager manager)
        {
            manager.GetLivingEntity()?.EntityStats.AddBonusMaxHealth(Value);
        }
    }
}
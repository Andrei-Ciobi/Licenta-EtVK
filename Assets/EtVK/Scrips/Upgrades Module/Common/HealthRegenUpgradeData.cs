using EtVK.Player_Module.Manager;
using EtVK.Upgrades_Module.Core;
using UnityEngine;

namespace EtVK.Upgrades_Module.Common
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Upgrades/Common/HealthRegen")]

    public class HealthRegenUpgradeData : CommonUpgradeData
    {
        private void Awake()
        {
            description = $"+{variableName} increased health regen";
            isPercentage = true;
        }
        public override void Action(PlayerManager manager)
        {
            manager.GetLivingEntity()?.EntityStats.AddBonusHealthRegen(ValuePercentage);
        }
    }
}
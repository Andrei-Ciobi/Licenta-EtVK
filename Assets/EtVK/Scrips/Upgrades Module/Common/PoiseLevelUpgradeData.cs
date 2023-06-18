using EtVK.Player_Module.Manager;
using EtVK.Upgrades_Module.Core;
using UnityEngine;

namespace EtVK.Upgrades_Module.Common
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Upgrades/Common/PoiseLevel")]
    public class PoiseLevelUpgradeData : CommonUpgradeData
    {
        private void Awake()
        {
            description = $"+{variableName} more damage resistance";
        }
        public override void Action(PlayerManager manager)
        {
            manager.GetLivingEntity()?.EntityStats.AddBonusPoiseLevel(Value);
        }
    }
}
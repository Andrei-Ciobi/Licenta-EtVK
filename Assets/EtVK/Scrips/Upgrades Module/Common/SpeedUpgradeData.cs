using EtVK.Player_Module.Manager;
using EtVK.Upgrades_Module.Core;
using UnityEngine;

namespace EtVK.Upgrades_Module.Common
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Upgrades/Common/Speed")]
    public class SpeedUpgradeData : CommonUpgradeData
    {
        private void Awake()
        {
            description = $"{variableName} more base movement speed";
        }

        public override void Action(PlayerManager manager)
        {
            manager.GetLocomotionData()?.AddBonusSpeed(Value);
        }
    }
}
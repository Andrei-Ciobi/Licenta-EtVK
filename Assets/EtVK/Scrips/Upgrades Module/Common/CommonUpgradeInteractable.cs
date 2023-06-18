using System.Collections.Generic;
using EtVK.Upgrades_Module.Core;

namespace EtVK.Upgrades_Module.Common
{
    public class CommonUpgradeInteractable : UpgradeInteractable
    {
        protected override List<BaseUpgradeData> GetUpgrades(int amount)
        {
            return UpgradesManager.Instance.GetCommonUpgrade(amount);
        }
    }
}
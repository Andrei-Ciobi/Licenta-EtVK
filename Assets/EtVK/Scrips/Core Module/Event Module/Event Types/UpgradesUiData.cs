using System.Collections.Generic;
using EtVK.Upgrades_Module.Core;

namespace EtVK.Event_Module.Event_Types
{
    [System.Serializable] public struct UpgradesUiData
    {
        public List<BaseUpgradeData> UpgradeList { get; set; }

        public UpgradesUiData(List<BaseUpgradeData> upgradeList)
        {
            UpgradeList = upgradeList;
        }
    }
}
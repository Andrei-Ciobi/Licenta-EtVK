using System.Collections.Generic;
using EtVK.Upgrades_Module.Common;
using EtVK.Upgrades_Module.Core;
using UnityEngine;

namespace EtVK.Player_Module.Manager
{
    public class PlayerUpgradesManager : MonoBehaviour
    {
        [SerializeField] private List<CommonUpgradeData> commonUpgrades;

        private PlayerManager manager;

        private void Awake()
        {
            manager = transform.root.GetComponent<PlayerManager>();
        }

        public void AddUpgrade(BaseUpgradeData upgradeData)
        {
            upgradeData.Action(manager);
            if (upgradeData is CommonUpgradeData commonUpgradeData)
            {
                commonUpgrades.Add(commonUpgradeData);
            }
        }

    }
}
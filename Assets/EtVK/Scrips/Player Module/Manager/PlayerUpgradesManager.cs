using System.Collections.Generic;
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

        public void AddCommonUpgrade(BaseUpgradeData upgradeData)
        {
            if(upgradeData is not CommonUpgradeData)
                return;
            
            upgradeData.Action(manager);
            commonUpgrades.Add((CommonUpgradeData) upgradeData);
        }

    }
}
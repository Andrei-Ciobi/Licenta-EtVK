using System.Collections.Generic;
using System.Linq;
using EtVK.Core;
using UnityEngine;

namespace EtVK.Upgrades_Module.Core
{
    public class UpgradesManager : MonoSingletone<UpgradesManager>
    {
        [SerializeField] private List<CommonUpgradeData> commonUpgradeList;

        private readonly int maxTries = 100;
        private void Awake()
        {
            InitializeSingletone();
            Initialize();
        }


        public BaseUpgradeData GetCommonUpgrade()
        {
            var randomIndex = Random.Range(0, commonUpgradeList.Count);
            return commonUpgradeList[randomIndex];
        }

        public List<BaseUpgradeData> GetCommonUpgrade(int numberOfUpgrades)
        {
            var upgrades = new List<BaseUpgradeData>();
            var upgradesAvailable = commonUpgradeList;

            for (var i = 0; i < numberOfUpgrades; ++i)
            {
                if(upgradesAvailable.Count == 0)
                    break;
                
                var randomIndex = Random.Range(0, upgradesAvailable.Count);
                upgrades.Add(commonUpgradeList[randomIndex]);
                upgradesAvailable.Remove(commonUpgradeList[randomIndex]);
            }

            return upgrades;
        }
        

        private void Initialize()
        {
            commonUpgradeList = Resources.LoadAll<CommonUpgradeData>("Upgrades/Common").ToList();
        }
    }
}
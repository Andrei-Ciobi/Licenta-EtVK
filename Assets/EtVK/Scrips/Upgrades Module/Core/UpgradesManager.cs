using System.Collections.Generic;
using System.Linq;
using EtVK.Core;
using EtVK.Upgrades_Module.Common;
using UnityEngine;

namespace EtVK.Upgrades_Module.Core
{
    public class UpgradesManager : MonoSingleton<UpgradesManager>
    {
        [SerializeField] private List<CommonUpgradeData> commonUpgradeList;

        private List<BaseUpgradeData> baseUpgradeList = new();

        private readonly int maxTries = 100;

        private void Awake()
        {
            InitializeSingleton();
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
            var upgradesAvailable = new List<CommonUpgradeData>(commonUpgradeList);

            for (var i = 0; i < numberOfUpgrades; ++i)
            {
                if (upgradesAvailable.Count == 0)
                    break;

                var randomIndex = Random.Range(0, upgradesAvailable.Count);
                upgrades.Add(commonUpgradeList[randomIndex]);
                upgradesAvailable.Remove(commonUpgradeList[randomIndex]);
            }

            return upgrades;
        }

        public T FindById<T>(int id) where T : BaseUpgradeData
        {
            return baseUpgradeList.Find(x => x is T && x.GetInstanceID() == id) as T;
        }

        public BaseUpgradeData FindById(int id)
        {
            return baseUpgradeList.Find(x => x.GetInstanceID() == id);
        }


        private void Initialize()
        {
            commonUpgradeList = Resources.LoadAll<CommonUpgradeData>("Upgrades/Common").ToList();
            baseUpgradeList.AddRange(commonUpgradeList);
        }
    }
}
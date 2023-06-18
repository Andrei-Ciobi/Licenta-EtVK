using EtVK.Upgrades_Module.Core;
using UnityEngine;

namespace EtVK.Upgrades_Module.Common
{
    public abstract class CommonUpgradeData : BaseUpgradeData
    {
        [Header("Common data")] 
        [SerializeField] private float value;
        [SerializeField] [Range(0, 100)] private int valuePercentage;
        [SerializeField] protected bool isPercentage;

        protected readonly string variableName = "{VALUE}";

        public float Value => value;
        public int ValuePercentage => valuePercentage;
        public bool IsPercentage => isPercentage;

        public override string GetDescriptionFormatted()
        {
            if (!description.Contains(variableName))
                return description;

            var valueString = isPercentage ? $"{valuePercentage}%" : $"{value}";
            return description.Replace(variableName, valueString);
        }
    }
}
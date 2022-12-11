using UnityEngine;

namespace EtVK.Upgrades_Module.Core
{
    public abstract class CommonUpgradeData : BaseUpgradeData
    {
        [Header("Common data")] [SerializeField]
        private float value;

        [SerializeField] private bool isPercentage;

        protected readonly string variableName = "{VALUE}";

        public float Value => value;
        public bool IsPercentage => isPercentage;

        public override string GetDescriptionFormatted()
        {
            if (!description.Contains(variableName))
                return description;

            var valueString = isPercentage ? $"{value}%" : $"{value}";
            return description.Replace(variableName, valueString);
        }
    }
}
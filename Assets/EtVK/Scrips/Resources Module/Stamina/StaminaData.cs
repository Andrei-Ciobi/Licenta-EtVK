using System.Collections.Generic;
using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Resources_Module.Stamina
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Stats/StaminaStats")]
    public class StaminaData : ScriptableObject
    {
        [SerializeField] private int stamina;
        [SerializeField] private float recuperateWaitTime;
        [SerializeField] private int recuperateWeight;
        [SerializeField] private List<SerializableSet<StaminaCostType, int>> costList;

        public int Stamina => stamina;

        public float RecuperateWaitTime => recuperateWaitTime;

        public int RecuperateWeight => recuperateWeight;

        public int GetCost(StaminaCostType costType)
        {
            var cost = costList.Find(x => x.GetKey() == costType);

            return cost?.GetValue() ?? 0;
        }
    }
}
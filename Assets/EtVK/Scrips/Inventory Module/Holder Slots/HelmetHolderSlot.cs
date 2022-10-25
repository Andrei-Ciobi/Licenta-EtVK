using System.Collections.Generic;
using UnityEngine;

namespace EtVK.Inventory_Module.Holder_Slots
{
    public class HelmetHolderSlot : ArmorHolderSlot
    {
        [SerializeField] private List<GameObject> dependencyList;

        public List<GameObject> DependencyList => dependencyList;
    }
}
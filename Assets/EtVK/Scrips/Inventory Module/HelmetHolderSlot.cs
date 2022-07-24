using System.Collections.Generic;
using UnityEngine;

namespace EtVK.Inventory_Module
{
    public class HelmetHolderSlot : ArmorHolderSlot
    {
        [SerializeField] private List<GameObject> dependencyList;

        public List<GameObject> DependencyList => dependencyList;
    }
}
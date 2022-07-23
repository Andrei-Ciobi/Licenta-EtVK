using System;
using UnityEngine;

namespace EtVK.Items_Module.Armors.Helmet
{
    public class Helmet : Armor
    {
        [SerializeField] private HelmetData helmetData;

        private void Awake()
        {
            armorData = helmetData;
            InitializeReferences();
        }
    }
}
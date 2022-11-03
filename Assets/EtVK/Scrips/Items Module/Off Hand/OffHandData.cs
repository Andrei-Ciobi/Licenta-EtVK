﻿using EtVK.Core.Utyles;
using EtVK.Inventory_Module;
using UnityEngine;

namespace EtVK.Items_Module.Off_Hand
{
    public abstract class OffHandData : ItemData
    {
        [SerializeField] protected OffHandType offHandType;
        public OffHandType OffHandType => offHandType;
    }
}
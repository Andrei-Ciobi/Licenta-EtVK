using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Resources_Module.Health
{
    public interface ILivingEntity
    {
        public Transform Transform { get; }
        public Factions EntityFaction { get; }
        public bool IsAllies(Factions faction);
    }
}
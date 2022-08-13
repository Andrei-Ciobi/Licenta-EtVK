using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Health_Module
{
    public interface ILivingEntity
    {
        public Transform Transform { get; }
        public Factions EntityFaction { get; }
        public bool IsAllies(Factions faction);
    }
}
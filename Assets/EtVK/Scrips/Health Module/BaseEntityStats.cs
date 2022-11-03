using System.Collections.Generic;
using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Health_Module
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Stats/EntityStats")]
    public class BaseEntityStats : ScriptableObject
    {
        [SerializeField] private string entityName; 
        [SerializeField] private float maxHealth;
        [SerializeField] private float poiseLevel;
        [SerializeField] private float invulnerableTime;
        [SerializeField] private Factions entityFaction;
        [SerializeField] private List<Factions> entityAllies;

        public string EntityName => entityName;

        public float MaxHealth => maxHealth;

        public float PoiseLevel => poiseLevel;

        public float InvulnerableTime => invulnerableTime;

        public Factions EntityFaction => entityFaction;

        public List<Factions> EntityAllies => entityAllies;
    }
}
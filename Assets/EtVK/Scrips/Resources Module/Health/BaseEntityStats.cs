using System;
using System.Collections.Generic;
using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Resources_Module.Health
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Stats/EntityStats")]
    public class BaseEntityStats : ScriptableObject
    {
        [SerializeField] private string entityName; 
        [SerializeField] private float maxHealth;
        [SerializeField] [Range(0, 100)] private int healthRegenPercentage;
        [SerializeField] [Range(0, 100)] private int maxHealthRegenPercentage;
        [SerializeField] private float poiseLevel;
        [SerializeField] private float invulnerableTime;
        [SerializeField] private Factions entityFaction;
        [SerializeField] private List<Factions> entityAllies;

        private float bonusMaxHealth;
        private float bonusPoiseLevel;

        public string EntityName => entityName;
        public float MaxHealth => maxHealth + bonusMaxHealth;
        public float HealthRegen => healthRegenPercentage / 100 * MaxHealth;
        public float PoiseLevel => poiseLevel + bonusPoiseLevel;
        public float InvulnerableTime => invulnerableTime;
        public Factions EntityFaction => entityFaction;
        public List<Factions> EntityAllies => entityAllies;

        public void AddBonusMaxHealth(float value)
        {
            bonusMaxHealth += value;
        }
        
        public void AddBonusPoiseLevel(float value)
        {
            bonusPoiseLevel += value;
        }

        public void AddBonusHealthRegen(int value)
        {
             healthRegenPercentage += value;
             Mathf.Clamp(healthRegenPercentage, 0, maxHealthRegenPercentage);
        }

        private void OnValidate()
        {
            if (healthRegenPercentage > maxHealthRegenPercentage)
            {
                healthRegenPercentage = maxHealthRegenPercentage;
            }
        }
    }
}
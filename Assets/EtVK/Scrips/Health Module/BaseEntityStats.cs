using UnityEngine;

namespace EtVK.Scrips.Health_Module
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Stats/EntityStats")]
    public class BaseEntityStats : ScriptableObject
    {
        [SerializeField] private string entityName; 
        [SerializeField] private float maxHealth;
        [SerializeField] private float poiseLevel;
        [SerializeField] private float invulnerableTime;

        public string EntityName => entityName;

        public float MaxHealth => maxHealth;

        public float PoiseLevel => poiseLevel;

        public float InvulnerableTime => invulnerableTime;
    }
}
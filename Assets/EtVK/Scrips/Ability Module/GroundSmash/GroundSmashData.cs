using EtVK.Ability_Module.Core;
using UnityEngine;

namespace EtVK.Ability_Module.GroundSmash
{
    [CreateAssetMenu(fileName ="New ability", menuName ="ScriptableObjects/Abilities/Damageable/GroundSmash")]
    public class GroundSmashData : BaseAbilityData
    {
        [Header("Ground Smash ability info")]
        [SerializeField] private float damage;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask hitLayer;

        public float Damage => damage;

        public float Radius => radius;

        public LayerMask HitLayer => hitLayer;
    }
}
using EtVK.Ability_Module.Core;
using EtVK.Health_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Ability_Module.GroundSmash
{
    public class GroundSmashAbility : BaseAbility
    {
        [Header("Debug")] [SerializeField] private float radius;
        ColorUsageAttribute color;

        private void Awake()
        {
            abilityType = AbilityType.GroundSmash;
        }

        public override void PerformAbility(BaseAbilityData baseAbilityData, Animator animator)
        {
            var groundSmashData = (GroundSmashData) baseAbilityData;

            var colliders = Physics.OverlapSphere(transform.position,
                groundSmashData.Radius, groundSmashData.HitLayer);

            // TO DO, implement the vfx

            // We search
            foreach (Collider collider in colliders)
            {

                if (collider.gameObject == this.gameObject)
                    continue;

                var damageableEntity = collider.GetComponent<IDamageable>();

                if (damageableEntity == null)
                    continue;

                // The owner should not get hit by the ability
                if (transform.root.gameObject != damageableEntity.GameObject)
                {
                    damageableEntity.TakeHit(groundSmashData.Damage);
                    Debug.Log("I hit " + damageableEntity.GameObject.name);
                }
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}
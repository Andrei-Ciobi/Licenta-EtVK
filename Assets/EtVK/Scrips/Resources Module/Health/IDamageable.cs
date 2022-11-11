using UnityEngine;

namespace EtVK.Resources_Module.Health
{
    public interface IDamageable
    {
        public GameObject GameObject { get; }
        public void TakeHit(float damage, string damageAnimation = "Base_damage_front", bool forceAnimation = false);
        public void Die();
    }
}
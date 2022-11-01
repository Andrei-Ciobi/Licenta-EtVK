using UnityEngine;

namespace EtVK.Health_Module
{
    public interface IDamageable
    {
        public GameObject GameObject { get; }
        public void TakeHit(float damage, string damageAnimation = "Base_damage_front", bool forceAnimation = false);
        public void Die();
    }
}
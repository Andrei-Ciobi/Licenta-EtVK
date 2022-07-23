using UnityEngine;

namespace EtVK.Health_Module
{
    public interface IDamageable
    {
        public GameObject GameObject { get; }
        public void TakeHit(float damage);
        public void Die();
    }
}
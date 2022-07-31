using UnityEngine;

namespace EtVK.Health_Module
{
    public class EnemyLivingEntity : LivingEntity
    {
        public delegate void OnDie();
        public OnDie onDie;
        public override void Die()
        {
            onDie?.Invoke();
            Destroy(gameObject);
        }
    }
}
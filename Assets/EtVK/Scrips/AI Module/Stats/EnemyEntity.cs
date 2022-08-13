using EtVK.Health_Module;

namespace EtVK.AI_Module.Stats
{
    public class EnemyEntity : LivingEntity<BaseEntityStats>
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
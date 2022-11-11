using EtVK.AI_Module.Managers;
using EtVK.Core.Controller;
using EtVK.Resources_Module.Health;

namespace EtVK.AI_Module.Stats
{
    public class EnemyEntity : LivingEntity<BaseEntityStats>
    {
        public delegate void OnDie();
        public OnDie onDie;

        private EnemyManager manager;

        public void Initialize(EnemyManager enemyManager)
        {
            manager = enemyManager;
        }

        protected override bool CanPlayDamageAnimation(float damage)
        {
            return !manager.UninterruptibleAction && base.CanPlayDamageAnimation(damage);
        }

        public override void Die()
        {
            onDie?.Invoke();
            Destroy(gameObject);
            var attackController = GetComponentInChildren<BaseAttackController>();
            if(attackController == null)
                return;
            
            attackController.Unsubscribe();
        }
    }
}
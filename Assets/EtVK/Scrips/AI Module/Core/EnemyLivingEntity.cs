﻿using EtVK.Health_Module;

namespace EtVK.AI_Module.Core
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
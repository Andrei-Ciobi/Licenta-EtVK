using EtVK.Event_Module.Events;
using UnityEngine;

namespace EtVK.Resources_Module.Health
{
    public class PlayerEntity : LivingEntity<BaseEntityStats>
    {
        [SerializeField] private FloatEvent updateHealthEvent;

        public override void TakeHit(float damage, string damageAnimation = "Base_damage_front", bool forceAnimation = false)
        {
            base.TakeHit(damage, damageAnimation, forceAnimation);
            if(godMode || IsInvulnerable)
                return;

            updateHealthEvent.Invoke(currentHealth / entityStats.MaxHealth);
        }
    }
}
using System;
using EtVK.Scrips.Items_Module.Weapons_Module;
using UnityEngine;

namespace EtVK.Scrips.Health_Module
{
    public class LivingEntity : MonoBehaviour, IDamageable
    {
        [SerializeField] private BaseEntityStats entityStats;

        [SerializeField] private bool isInvulnerable;

        private float currentHealth;
        private float currentPoiseLevel;

        private bool damageAnimationOnCd;

        private void Start()
        {
            OnStart();
        }


        private void OnStart()
        {
            currentHealth = entityStats.MaxHealth;
        }

        public void TakeHit(float damage)
        {
            if (isInvulnerable)
                return;
            
            currentHealth -= damage;
            
            // Object needs to be destroyed from an animation event
            if (currentHealth <= 0)
            {
                Die();
                return;
            }
            
            //isInvulnerable = true;

            Debug.Log(this.gameObject.name + " recived " + damage);
            
            // if (CanPlayDamageAnimation(damage))
            //     animator.SetTrigger(EnemyAIAction.TakeDamage.ToString());
            // else
            //     StartCoroutine(InvulnerableCorutine(stats.GetInvulnerableTime()));
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.gameObject == gameObject)
                return;
            
            var weapon = other.gameObject.GetComponent<IWeaponDamageable>();
            
            if(weapon == null)
                return;
            
            
            TakeHit(weapon.DealDamage());
        }
    }
}
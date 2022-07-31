using EtVK.Items_Module.Weapons;
using UnityEngine;

namespace EtVK.Health_Module
{
    public class LivingEntity : MonoBehaviour, IDamageable
    {
        [SerializeField] private BaseEntityStats entityStats;
        [SerializeField] private bool isInvulnerable;
        
        public GameObject GameObject => gameObject;

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



        public virtual void TakeHit(float damage)
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

            Debug.Log(this.GameObject.name + " recived " + damage);
            
            // if (CanPlayDamageAnimation(damage))
            //     animator.SetTrigger(EnemyAIAction.TakeDamage.ToString());
            // else
            //     StartCoroutine(InvulnerableCorutine(stats.GetInvulnerableTime()));
        }

        public virtual void Die()
        {
            Destroy(GameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.gameObject == GameObject)
                return;
            
            var weapon = other.gameObject.GetComponent<IWeaponDamageable>();
            
            if(weapon == null)
                return;
            
            
            TakeHit(weapon.DealDamage());
        }
    }
}
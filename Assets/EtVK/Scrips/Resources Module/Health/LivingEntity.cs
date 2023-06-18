using System.Collections;
using System.Collections.Generic;
using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Items_Module.Weapons;
using EtVK.Resources_Module.Stamina;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EtVK.Resources_Module.Health
{
    public abstract class LivingEntity<TStats> : MonoBehaviour, IDamageable, ILivingEntity
        where TStats : BaseEntityStats
    {
        [SerializeField] protected TStats entityStats;
        [SerializeField] protected bool godMode;

        [SerializeField] protected List<SerializableSet<string, int>> damageAnimationVariation = new()
        {
            new SerializableSet<string, int>("Base_damage_front", 3),
            new SerializableSet<string, int>("Base_damage_back", 3),
            new SerializableSet<string, int>("Base_damage_right", 3),
            new SerializableSet<string, int>("Base_damage_left", 3),
        };

        public bool IsInvulnerable
        {
            get => isInvulnerable;
            set => isInvulnerable = value;
        }

        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        public Factions EntityFaction => entityStats.EntityFaction;
        public TStats EntityStats => entityStats;

        private Animator animator;

        protected float currentHealth;
        private float currentPoiseLevel;
        private bool damageAnimationOnCd;
        private bool isInvulnerable;

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        public virtual void TakeHit(float damage, string damageAnimation = "Base_damage_front", bool forceAnimation = false)
        {
            if(godMode || isInvulnerable)
                return;
            currentHealth -= damage;
            
            // Object needs to be destroyed from an animation event
            if (currentHealth <= 0)
            {
                Die();
                return;
            }

           
            if (string.IsNullOrEmpty(damageAnimation))
            {
                Debug.LogError("No correct damage animation given");
                return;
            }

            if (forceAnimation)
            {
                animator.CrossFade(damageAnimation, 0f);
                return;
            }
            
            if (CanPlayDamageAnimation(damage))
            {
                animator.CrossFade(damageAnimation, 0f);
            }
            else
            {
                StartCoroutine(InvulnerableCoroutine(entityStats.InvulnerableTime));
            }
            
            // Debug.Log("Hit damage taken = " + damage);
        }

        public virtual void Die()
        {
            Destroy(GameObject);
        }

        public bool IsAllies(Factions faction)
        {
            return entityStats.EntityAllies.Contains(faction) || entityStats.EntityFaction.Equals(faction);
        }

        private IEnumerator InvulnerableCoroutine(float time)
        {
            isInvulnerable = true;
            yield return new WaitForSecondsRealtime(time);
            isInvulnerable = false;
        }
        
        private string CalculateDamageDirectionAnimation(float direction)
        {
            return direction switch
            {
                >= 145 and <= 180 => "Base_damage_front",
                <= -145 and >= -180 => "Base_damage_front",
                >= -45 and <= 45 => "Base_damage_back",
                >= -145 and <= -45 => "Base_damage_right",
                >= 45 and <= 145 => "Base_damage_left",
                _ => null
            };
        }

        protected virtual bool CanPlayDamageAnimation(float damage)
        {
            currentPoiseLevel -= damage;

            if (currentPoiseLevel > 0)
                return false;

            currentPoiseLevel = entityStats.PoiseLevel;
            return true;
        }

        private void OnStart()
        {
            currentHealth = entityStats.MaxHealth;
        }

        private void OnAwake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.gameObject == GameObject)
                return;

            var weapon = other.gameObject.GetComponent<IWeaponDamageable>();

            if (weapon == null)
                return;

            // var contactPoint = other.ClosestPointOnBounds(transform.position);
            var directionHit = Vector3.SignedAngle(other.transform.root.forward, transform.forward, Vector3.up);

            var blockingManager = transform.GetComponentInChildren<BlockingManager>();
            var staminaManager = transform.GetComponentInChildren<StaminaManager>();
            
            if (blockingManager != null)
            {
                var canBlock = staminaManager?.CheckCanPerformAction(StaminaCostType.Block) ?? true;
                if (blockingManager.CheckBlockingStatus(directionHit) && canBlock)
                {
                    var damage = blockingManager.CalculateNewDamage(weapon.DealDamage());
                    TakeHit(damage, "Block", true);
                    animator.SetLayerWeight(blockingManager.BlockingLayer, 0f);
                    
                    staminaManager?.PerformStaminaDrain(StaminaCostType.Block);
                    return;
                }
                
                if (blockingManager.IsBlocking)
                {
                    animator.SetBool(WeaponActionType.Block.ToString(), false);
                    animator.SetBool(AnimatorCommonFileds.IsAimAction.ToString(), false);
                
                }
            }

           
            
            // Decide the damage animation 
            var damageAnimation = CalculateDamageDirectionAnimation(directionHit);
            var range = damageAnimationVariation.Find(x => x.GetKey() == damageAnimation).GetValue();
            var randomAnimationIndex = Random.Range(0, range);
            damageAnimation = damageAnimation + "_" + randomAnimationIndex;

            TakeHit(weapon.DealDamage(), damageAnimation);
        }

      
    }
}
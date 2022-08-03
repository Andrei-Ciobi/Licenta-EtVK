using EtVK.Inventory_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Weapons
{
    public abstract class BaseEnemyWeaponData : ItemData
    {
        [SerializeField] protected WeaponType weaponType;
        [SerializeField] private AnimatorOverrideController animatorOverride;

        [Header("Weapon stats")] 
        [Range(0f, 35f)] [SerializeField] private float attackRange;
        [Range(0f, 35f)] [SerializeField] private float meleeRange;
        [Range(0f, 10f)] [SerializeField] private float weaponAttackCd;

        public WeaponType WeaponType => weaponType;
        public float AttackRange => attackRange;
        public float MeleeRange => meleeRange;
        public float WeaponAttackCd => weaponAttackCd;
        public AnimatorOverrideController AnimatorOverride => virtualOverride;
        
        private AnimatorOverrideController virtualOverride;

        public virtual float Damage => 0f;
        public abstract void Initialize();
        protected void InitializeVirtualAnimator()
        {
            if(animatorOverride == null)
                return;

            virtualOverride = new AnimatorOverrideController(animatorOverride.runtimeAnimatorController);

            for (var i = 0; i < animatorOverride.animationClips.Length; ++i)
            {
                var clipName = animatorOverride.runtimeAnimatorController.animationClips[i].name;
                var clip = animatorOverride.animationClips[i];

                virtualOverride[clipName] = clip;
            }
        }
    }
}
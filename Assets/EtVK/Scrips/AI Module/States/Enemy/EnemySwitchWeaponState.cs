using EtVK.AI_Module.Actions;
using EtVK.AI_Module.Managers;
using EtVK.Core;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.States
{
    public class EnemySwitchWeaponState : SceneLinkedSMB<EnemyManager>
    {
        [Header("Bool for draw/withdraw weapon, default: draw weapon")]
        [SerializeField] private bool withdrawWeapon;

        private WeaponAction action;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (withdrawWeapon)
            {
                WithdrawWeapon(animator);
            }
            else
            {
                DrawWeapon(animator);
            }
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(action == null)
                return;
            
            action.LayerIndexList.ForEach(x => animator.SetLayerWeight(x.GetHashCode(), 0f));
        }

        private void DrawWeapon(Animator animator)
        {
            action = null;
            // TO DO: manager for deciding the weapon to draw
            const WeaponType type = WeaponType.Sword;

            var weapon = monoBehaviour.GetInventoryManager()
                .GetWeapon(weapon => weapon.WeaponData.WeaponType.Equals(type));
            action = weapon.WeaponData.GetBaseActionAs<WeaponAction>(x =>
                x.WeaponType == type && x.WeaponActionType == WeaponActionType.Draw);
            
            action.LayerIndexList.ForEach(x => animator.SetLayerWeight(x.GetHashCode(), 1f));
            
            
            weapon.SetAnimationOverride();
            animator.runtimeAnimatorController = weapon.WeaponData.AnimatorOverride;
        }

        private void WithdrawWeapon(Animator animator)
        {
            var weapon = monoBehaviour.GetInventoryManager().GetCurrentWeapon();
            if(weapon == null)
                return;
            
            animator.runtimeAnimatorController = monoBehaviour.BaseAnimatorOverrideController;
        }
    }
}
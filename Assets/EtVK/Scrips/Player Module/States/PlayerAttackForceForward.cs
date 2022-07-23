using EtVK.Attacks_Module;
using EtVK.Core_Module;
using EtVK.Player_Module.Controller;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerAttackForceForward : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] private PlayerAttackState attackState;
        private ForceForwardAttackAction attackAction;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var weapon = monoBehaviour.GetInventoryManager().GetArmedWeapon();
            attackAction =
                (ForceForwardAttackAction) weapon.WeaponData.GetAttackAction(attackState.AttackType, attackState.AttackIndex - 1);

            if (attackAction == null)
            {
                Debug.LogError("Not an force forward attack action");
            }
            
        }

        public override void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           ForceForward(stateInfo);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ForceForward(stateInfo);
        }

        private void ForceForward(AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime < Mathf.Clamp(stateInfo.length, 0f, 1f) - attackAction.Interval &&
                attackAction.UseForceForward)
            {
                monoBehaviour.GetController()
                    .UpdateForceMovementForward(attackAction.Speed *
                                                attackAction.SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }
        
    }
}
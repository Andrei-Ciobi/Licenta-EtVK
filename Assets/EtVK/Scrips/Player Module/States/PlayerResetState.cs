using EtVK.Core;
using EtVK.Core.Utyles;
using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerResetState : SceneLinkedSMB<PlayerManager>
    {
        [SerializeField] private bool resetAimState;
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetAnimationEventManager().SetCanCombo(0);
            monoBehaviour.GetAnimationEventManager().DeactivateWeaponCollider();

            if (resetAimState)
            {
                monoBehaviour.SetAimActionState(WeaponActionType.Block, false, 0);
                monoBehaviour.SetAimActionState(WeaponActionType.Aim, false, 0);
            }
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            monoBehaviour.GetAnimationEventManager().SetCanCombo(0);
            monoBehaviour.GetAnimationEventManager().DeactivateWeaponCollider();
            if (resetAimState)
            {
                monoBehaviour.SetAimActionState(WeaponActionType.Block, false, 0);
                monoBehaviour.SetAimActionState(WeaponActionType.Aim, false, 0);
            }
        }
    }
}
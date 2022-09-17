using EtVK.Core;
using EtVK.Input_Module;
using EtVK.Player_Module.Controller;
using UnityEngine;

namespace EtVK.Player_Module.States
{
    public class PlayerSwitchWeaponState : SceneLinkedSMB<PlayerManager>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (InputManager.Instance.Player.SwitchWeaponInput)
            {
                InputManager.Instance.Player.SwitchWeaponInput = false;
                var weaponType = InputManager.Instance.Player.SwitchWeaponType;
                var currentWeaponArmed = monoBehaviour.GetInventoryManager().GetCurrentWeapon();
                var weaponToSwitch = monoBehaviour.GetInventoryManager().GetWeaponByType(weaponType);
                
                if (weaponToSwitch == null)
                {
                    Debug.Log($"No such weapon in inventory: {weaponType.ToString()}");
                    return;
                }
                
                weaponToSwitch.SwitchWeapon(currentWeaponArmed);
                
                var newWeapon = monoBehaviour.GetInventoryManager().GetCurrentWeapon();

                animator.runtimeAnimatorController = newWeapon != null ? newWeapon.WeaponData.AnimatorOverride : monoBehaviour.BaseAnimatorOverrideController;
            }
        }
    }
}
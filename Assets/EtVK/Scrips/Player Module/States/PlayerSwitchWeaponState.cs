using EtVK.Scrips.Core_Module;
using EtVK.Scrips.Input_Module;
using EtVK.Scrips.Player_Module.Controller;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.States
{
    public class PlayerSwitchWeaponState : SceneLinkedSMB<PlayerManager>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (InputManager.Instance.SwitchWeaponInput)
            {
                InputManager.Instance.SwitchWeaponInput = false;
                var weaponType = InputManager.Instance.SwitchWeaponType;
                var curentWeaponArmed = monoBehaviour.GetInventoryManager().GetArmedWeapon();
                var weaponToSwitch = monoBehaviour.GetInventoryManager().GetWeapon(weaponType);
                
                if (weaponToSwitch == null)
                {
                    Debug.Log($"No such weapon in inventory: {weaponType.ToString()}");
                    return;
                }
                
                weaponToSwitch.SwitchWeapon(curentWeaponArmed);
                
                var newWeapon = monoBehaviour.GetInventoryManager().GetArmedWeapon();

                if (newWeapon != null)
                {
                    animator.runtimeAnimatorController = newWeapon.WeaponData.AnimatorOverride;
                }
                else
                {
                    animator.runtimeAnimatorController = monoBehaviour.BaseAnimatorOverrideController;
                }
            }
        }
    }
}
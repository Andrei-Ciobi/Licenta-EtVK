using EtVK.Ability_Module;
using EtVK.Items_Module.Weapons;
using EtVK.Player_Module.Controller;
using UnityEngine;

namespace EtVK.Core_Module
{
    public class AnimationEventManager : MonoBehaviour
    {
        public bool CanCombo => canCombo;

        private bool canCombo;


        public void SetCanCombo(int value)
        {
            var boolValue = value != 0;
            canCombo = boolValue;
        }


        public void ActivateWeaponCollider()
        {
            var playerManager = transform.root.GetComponent<PlayerManager>();

            var weaponColliderController = playerManager.GetInventoryManager().GetArmedWeapon()
                .GetComponent<WeaponColliderController>();
            
            weaponColliderController.ActivateColliders();

        }
        
        public void DeactivateWeaponCollider()
        {
            var playerManager = transform.root.GetComponent<PlayerManager>();

            var weaponColliderController = playerManager.GetInventoryManager().GetArmedWeapon()
                .GetComponent<WeaponColliderController>();
            
            weaponColliderController.DeactivateColliders();

        }

        public void PerformAbility(BaseAbilityData abilityData)
        {
            if(abilityData == null)
            {
                Debug.LogError("No ability data given in the aniamtion event");
                return;
            }
            
            var playerManager = transform.root.GetComponent<PlayerManager>();
            var ability = playerManager.GetAbility(abilityData.AbilityType);
            
            ability.PerformAbility(abilityData);
        }
    }
}
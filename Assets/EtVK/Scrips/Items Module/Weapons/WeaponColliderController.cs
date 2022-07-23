using UnityEngine;

namespace EtVK.Items_Module.Weapons
{
    public class WeaponColliderController : MonoBehaviour
    {
        private Collider[] weaponColliders;
        
        private void Awake()
        {
            weaponColliders = GetComponents<Collider>();
        }

        public void ActivateColliders()
        {
            foreach (var weaponCollider in weaponColliders)
            {
                weaponCollider.enabled = true;
            }
        }
        
        public void DeactivateColliders()
        {
            foreach (var weaponCollider in weaponColliders)
            {
                weaponCollider.enabled = false;
            }
        }
    }
}
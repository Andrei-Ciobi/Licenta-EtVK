using EtVK.Actions_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Actions
{
    [CreateAssetMenu( menuName = "ScriptableObjects/AI/Actions/WeaponAction")]
    public class WeaponAction : BaseAction
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private WeaponActionType weaponActionType;
        [SerializeField] private int layerIndex;
        
        
        public WeaponType WeaponType => weaponType;

        public WeaponActionType WeaponActionType => weaponActionType;

        public int LayerIndex => layerIndex;
    }
}
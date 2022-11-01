using System.Collections.Generic;
using EtVK.Actions_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Actions
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Actions/WeaponAction")]
    public class WeaponAction : BaseAction
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private WeaponActionType weaponActionType;
        [SerializeField] private List<AnimatorLayer> layerIndexList;
        
        public WeaponType WeaponType => weaponType;
        public WeaponActionType WeaponActionType => weaponActionType;
        public List<AnimatorLayer> LayerIndexList => layerIndexList;
    }
}
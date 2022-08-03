using System.Collections.Generic;
using EtVK.AI_Module.Actions;
using UnityEngine;

namespace EtVK.AI_Module.Weapons
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/AI/Weapons/EnemyWeaponData")]
    public class EnemyWeaponData : BaseEnemyWeaponData
    {
        [Header("List of attack actions")] 
        [SerializeField] private List<EnemyAttackAction> attackActionList;
        
        public override void Initialize()
        {
            InitializeVirtualAnimator();
        }

        public List<EnemyAttackAction> GetAttackActions()
        {
            return attackActionList;
        }
    }
}
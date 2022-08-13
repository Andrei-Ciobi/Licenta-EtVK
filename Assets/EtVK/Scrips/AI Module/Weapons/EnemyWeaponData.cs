using EtVK.AI_Module.Actions;
using UnityEngine;

namespace EtVK.AI_Module.Weapons
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/AI/Weapons/EnemyWeaponData")]
    public class EnemyWeaponData : BaseEnemyWeaponData<EnemyAttackAction>
    {
        public override void Initialize()
        {
            InitializeVirtualAnimator();
        }
    }
}
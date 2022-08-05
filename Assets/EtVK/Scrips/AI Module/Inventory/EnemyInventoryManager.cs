using EtVK.AI_Module.Actions;
using EtVK.AI_Module.Weapons;

namespace EtVK.AI_Module.Inventory
{
    public class EnemyInventoryManager : BaseEnemyInventoryManager<EnemyWeapon, EnemyWeaponData, EnemyAttackAction>
    {
        private void Awake()
        {
            Initialize();
        }
    }
}
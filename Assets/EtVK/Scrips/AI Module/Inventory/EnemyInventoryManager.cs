using EtVK.AI_Module.Weapons;

namespace EtVK.AI_Module.Inventory
{
    public class EnemyInventoryManager : BaseEnemyInventoryManager<EnemyWeapon, EnemyWeaponData>
    {
        private void Awake()
        {
            Initialize();
        }
    }
}
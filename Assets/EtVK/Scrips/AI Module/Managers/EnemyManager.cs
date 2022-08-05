using EtVK.AI_Module.Controllers;
using EtVK.AI_Module.Inventory;
using EtVK.AI_Module.Stats;

namespace EtVK.AI_Module.Managers
{
    public class EnemyManager : BaseEnemyManager<EnemyManager, 
                                                EnemyController, 
                                                EnemyInventoryManager,
                                                EnemyLocomotionData,
                                                EnemyEntity>
    {
        private void Awake()
        {
            InitializeBaseReferences();
            rootMotionController.Initialize(this);
        }

        private void Start()
        {
            OnStart();
        }
    }
    
}
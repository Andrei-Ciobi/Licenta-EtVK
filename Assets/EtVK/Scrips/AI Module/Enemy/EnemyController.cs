using System;
using EtVK.Health_Module;
using UnityEngine;

namespace EtVK.AI_Module.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public Transform CurrentTarget { get; set; }

        private EnemyManager enemyManager;

        private void Awake()
        {
            InitializeReferences();
        }

        // Function that detect if a living entity is in our field of view
        public void HandleDetection()
        {
            //If we already have a target we don't want to get another one
            if (CurrentTarget != null)
                return;

            var colliders = Physics.OverlapSphere(transform.position,
                enemyManager.GetLocomotionData().BaseDetectionRadius, enemyManager.GetLocomotionData().DetectionLayer);
            
            foreach (var coll in colliders)
            {
                if (coll.gameObject == this.gameObject) continue;

                var livingEntity = coll.GetComponent<LivingEntity>();

                if (livingEntity == null) 
                    continue;
                
                // If both entities are part of the same factions or part of an ally faction, they can't fight each other
                if (enemyManager.GetLivingEntity().IsAllies(livingEntity.EntityFaction))
                    continue;

                // We calculate the direction from this gameObj to the target
                var directionToTarget = (livingEntity.transform.position - transform.position).normalized;
                var distanceToTarget = Vector3.Distance(livingEntity.transform.position, transform.position);

                //If the target is not in the field of view or he is in aggro range
                if (!(Vector3.Angle(transform.forward, directionToTarget) <
                      enemyManager.GetLocomotionData().DetectionAngle / 2) &&
                    !(distanceToTarget < enemyManager.GetLocomotionData().AggroRange))
                    continue;
                
                // We check if we have obstacles in our path
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget,
                        enemyManager.GetLocomotionData().ObstacleMask))
                    continue;
                
                CurrentTarget = livingEntity.transform;
                enemyManager.LookingForTarget = false;
            }
        }

        private void InitializeReferences()
        {
            enemyManager = GetComponent<EnemyManager>();
        }
    }
}
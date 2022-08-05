using EtVK.AI_Module.Managers;
using EtVK.Health_Module;
using UnityEngine;

namespace EtVK.AI_Module.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        public Transform CurrentTarget { get; set; }
        public bool HasCurrentTarget => CurrentTarget;

        private EnemyManager enemyManager;

        private void Awake()
        {
            InitializeReferences();
        }
        
        public virtual void FixedUpdate()
        {
            if (!enemyManager.IsChasing)
                return;
            
            if (HasCurrentTarget)
            {
                enemyManager.GetNavMeshAgent().destination = CurrentTarget.position;
            }

        }

        public void Move(Vector3 position)
        {
            transform.root.position = position;
        }

        public void MoveAgent(bool status, float speed = 0f)
        {
            enemyManager.GetNavMeshAgent().isStopped = !status;
            enemyManager.IsChasing = status;
            if (status)
            {
                enemyManager.GetNavMeshAgent().speed = speed;
            }
        }

        public void UpdateRootMotionRotation(Animator animator)
        {
            
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

                var livingEntity = coll.GetComponent<ILivingEntity>();

                if (livingEntity == null) 
                    continue;
                
                // If both entities are part of the same factions or part of an ally faction, they can't fight each other
                if (enemyManager.GetLivingEntity().IsAllies(livingEntity.EntityFaction))
                    continue;

                // We calculate the direction from this gameObj to the target
                var directionToTarget = (livingEntity.Transform.position - transform.position).normalized;
                var distanceToTarget = Vector3.Distance(livingEntity.Transform.position, transform.position);

                //If the target is not in the field of view or he is in aggro range
                if (!(Vector3.Angle(transform.forward, directionToTarget) <
                      enemyManager.GetLocomotionData().DetectionAngle / 2) &&
                    !(distanceToTarget < enemyManager.GetLocomotionData().AggroRange))
                    continue;
                
                // We check if we have obstacles in our path
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget,
                        enemyManager.GetLocomotionData().ObstacleMask))
                    continue;
                
                CurrentTarget = livingEntity.Transform;
                enemyManager.LookingForTarget = false;
            }
        }

        public void RotateTowardsCurrentTarget()
        {
            var direction = DirectionToCurrentTarget;
            direction.y = 0;
            direction.Normalize();
            
            if (direction == Vector3.zero)
                direction = transform.forward;

            var locomotionData = enemyManager.GetLocomotionData();
            
            var reverseDistance = locomotionData.CombatAggroRange - DistanceToCurrentTarget;
            var rotateSpeed = Mathf.Clamp(reverseDistance, locomotionData.RotationSpeed, locomotionData.MaxRotationSpeed);
            
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
        public float AngleBetweenGivenTarget(Transform currentTransform, Vector3 lookTarget)
        {
            var directionToTarget = (lookTarget - currentTransform.position).normalized;
            var targetAngle = 90 - Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;

            return Mathf.DeltaAngle(currentTransform.eulerAngles.y, targetAngle);
        }

        public bool TargetInRange(float range)
        {
            if (ObstructedVisionTowardsCurrentTarget())
                return false;
            return DistanceToCurrentTarget < range;
        }
        
        public bool ObstructedVisionTowardsCurrentTarget()
        {
            return CurrentTarget == null ||
                   Physics.Raycast(transform.position, DirectionToCurrentTarget, DistanceToCurrentTarget,
                       enemyManager.GetLocomotionData().ObstacleMask);

        }
        private void InitializeReferences()
        {
            enemyManager = GetComponent<EnemyManager>();
        }
        
        public Vector3 DirectionToCurrentTarget => CurrentTarget.position - enemyManager.transform.position;
        
        public  float DistanceToCurrentTarget{
            get
            {
                var vectorToTarget = transform.position - CurrentTarget.position;
                vectorToTarget.y = 0f;
                return vectorToTarget.magnitude;
            }
        }
    }
}
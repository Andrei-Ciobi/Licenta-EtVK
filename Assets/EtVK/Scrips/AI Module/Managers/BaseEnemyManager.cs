using System.Collections;
using EtVK.AI_Module.Controllers;
using EtVK.Core_Module;
using UnityEngine;
using UnityEngine.AI;

namespace EtVK.AI_Module.Managers
{
    public class BaseEnemyManager<TManager, TController, TInventory, TLocomotionData, TEntity>
        : BaseManager<TManager, TController, TInventory, TEntity>
        where TManager 
        : BaseEnemyManager<TManager, TController, TInventory, TLocomotionData, TEntity>
    {
        [SerializeField] private TLocomotionData locomotionData;
        [Header("Set AI to dummy mode, only takes damage")]
        [SerializeField] private bool dummyMode;
        
        public bool UseRootMotionRotation { get; set; }
        public bool LookingForTarget { get; set; }
        public bool IsChasing { get; set; }
        public bool CanAttack { get; set; }
        
        private Rigidbody agentRigidBody;
        private NavMeshAgent navMeshAgent;
        private PatrolManager patrolManager;
        private EnemyAnimationEventController animationEventController;
        protected EnemyRootMotionController rootMotionController;
        
        
        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public void SetAttackOnCd(float time)
        {
            StartCoroutine(AttackCdCoroutine(time));
        }
        public bool HasPatrolPath()
        {
            return patrolManager != null && patrolManager.HasPath;
        }

        public TLocomotionData GetLocomotionData()
        {
            return locomotionData;
        }
        
        public Rigidbody GetAgentRigidBody()
        {
            return agentRigidBody;
        }
        
        public NavMeshAgent GetNavMeshAgent()
        {
            return navMeshAgent;
        }
        
        public PatrolManager GetPatrolManager()
        {
            return patrolManager;
        }

        public EnemyAnimationEventController GetAnimationEventController()
        {
            return animationEventController;
        }

        protected override void InitializeBaseReferences()
        {
            base.InitializeBaseReferences();
            agentRigidBody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            patrolManager = GetComponentInChildren<PatrolManager>();
            rootMotionController = GetComponentInChildren<EnemyRootMotionController>();
            animationEventController = GetComponentInChildren<EnemyAnimationEventController>();
        }

        protected void OnStart()
        {
            agentRigidBody.isKinematic = true;
            CanAttack = true;
        }

        private IEnumerator AttackCdCoroutine(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            CanAttack = true;
        }
    }
}
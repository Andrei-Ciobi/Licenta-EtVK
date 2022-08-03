using EtVK.AI_Module.Controllers;
using EtVK.AI_Module.Core;
using EtVK.AI_Module.Inventory;
using EtVK.Core_Module;
using UnityEngine;
using UnityEngine.AI;

namespace EtVK.AI_Module.Managers
{
    public class EnemyManager : BaseManager<EnemyManager, EnemyController, EnemyInventoryManager>
    {
        [SerializeField] private EnemyLocomotionData enemyLocomotionData;
        [Header("Set AI to dummy mode, only takes damage")]
        [SerializeField] private bool dummyMode;

        public bool UseRootMotionRotation { get; set; }
        public bool LookingForTarget { get; set; }
        public bool IsChasing { get; set; }
        
        private EnemyRootMotionController rootMotionController;
        private Rigidbody agentRigidBody;
        private EnemyLivingEntity livingEntity;
        private NavMeshAgent navMeshAgent;
        private PatrolManager patrolManager;

        private void Awake()
        {
            InitializeBaseReferences();
            InitializeReferences();
        }

        private void Start()
        {
            OnStart();
        }

        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public bool HasPatrolPath()
        {
            return patrolManager != null && patrolManager.HasPath;
        }

        

        public Rigidbody GetAgentRigidBody()
        {
            return agentRigidBody;
        }

        public EnemyLivingEntity GetLivingEntity()
        {
            return livingEntity;
        }

        public NavMeshAgent GetNavMeshAgent()
        {
            return navMeshAgent;
        }
        
        public EnemyLocomotionData GetLocomotionData()
        {
            return enemyLocomotionData;
        }

        public PatrolManager GetPatrolManager()
        {
            return patrolManager;
        }


        private void InitializeReferences()
        {
            rootMotionController = GetComponentInChildren<EnemyRootMotionController>();
            agentRigidBody = GetComponent<Rigidbody>();
            livingEntity = GetComponentInChildren<EnemyLivingEntity>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            patrolManager = GetComponentInChildren<PatrolManager>();
            
            rootMotionController.Initialize(this);

        }

        private void OnStart()
        {
            agentRigidBody.isKinematic = true;
        }
    }
    
}
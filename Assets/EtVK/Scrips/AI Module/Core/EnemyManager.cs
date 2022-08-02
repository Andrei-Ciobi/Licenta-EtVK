using EtVK.AI_Module.Inventory;
using EtVK.Core_Module;
using EtVK.Inventory_Module;
using UnityEngine;
using UnityEngine.AI;

namespace EtVK.AI_Module.Core
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyLocomotionData enemyLocomotionData;
        [Header("Set AI to dummy mode, only takes damage")]
        [SerializeField] private bool dummyMode;

        public bool UseRootMotionRotation { get; set; }
        public bool LookingForTarget { get; set; }
        public bool IsChasing { get; set; }

        private EnemyController controller;
        private Animator animator;
        private EnemyRootMotionController rootMotionController;
        private Rigidbody agentRigidBody;
        private EnemyLivingEntity livingEntity;
        private NavMeshAgent navMeshAgent;
        private PatrolManager patrolManager;
        private EnemyInventoryManager inventoryManager;

        private void Awake()
        {
            InitializeReferences();
            SceneLinkedSMB<EnemyManager>.Initialise(animator, this);
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
        public EnemyController GetController()
        {
            return controller;
        }

        public Animator GetAnimator()
        {
            return animator;
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
        public EnemyInventoryManager GetInventoryManager()
        {
            return inventoryManager;
        }
        

        private void InitializeReferences()
        {
            controller = GetComponent<EnemyController>();
            animator = GetComponentInChildren<Animator>();
            rootMotionController = GetComponentInChildren<EnemyRootMotionController>();
            agentRigidBody = GetComponent<Rigidbody>();
            livingEntity = GetComponentInChildren<EnemyLivingEntity>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            patrolManager = GetComponentInChildren<PatrolManager>();
            inventoryManager = GetComponentInChildren<EnemyInventoryManager>();
            
            rootMotionController.Initialize(this);

        }

        private void OnStart()
        {
            agentRigidBody.isKinematic = true;
        }
    }
    
}
using System;
using EtVK.Core_Module;
using EtVK.Health_Module;
using EtVK.Inventory_Module;
using UnityEngine;
using UnityEngine.AI;

namespace EtVK.AI_Module.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyLocomotionData enemyLocomotionData;
        [Header("Set AI to dummy mode, only takes damage")]
        [SerializeField] private bool dummyMode;

        public bool UseRootMotionRotation { get; set; }
        public bool LookingForTarget { get; set; }

        private EnemyController controller;
        private Animator animator;
        private InventoryManager inventoryManager;
        private Rigidbody agentRigidBody;
        private EnemyLivingEntity livingEntity;
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            InitializeReferences();
            SceneLinkedSMB<EnemyManager>.Initialise(animator, this);
        }

        private void Start()
        {
            OnStart();
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
        
        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
        private void InitializeReferences()
        {
            controller = GetComponent<EnemyController>();
            animator = GetComponentInChildren<Animator>();
            inventoryManager = GetComponentInChildren<InventoryManager>();
            agentRigidBody = GetComponent<Rigidbody>();
            livingEntity = GetComponentInChildren<EnemyLivingEntity>();
            navMeshAgent = GetComponent<NavMeshAgent>();

        }

        private void OnStart()
        {
            agentRigidBody.isKinematic = true;
        }
    }
    
}
using EtVK.Core_Module;
using UnityEngine;

namespace EtVK.AI_Module.Core
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AI/Stats/EnemyLocomotionData")]
    public class EnemyLocomotionData : LocomotionData
    {
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float maxRotationSpeed = 4f;
        [Range(0f, 420f)] [SerializeField] private float patrolWaitTime;

        [Header("Detection parameters")] 
        [SerializeField] private LayerMask detectionLayer;
        [SerializeField] private LayerMask obstacleMask;
        [Header("Agent viewable angle")]
        [Range(0f, 360f)] [SerializeField] private float detectionAngle; // The angle from which the agent can see a target
        [Header("Agent detection radius for enemy")]
        [Range(0f, 35f)] [SerializeField] private float baseDetectionRadius; // The distance from which the agent starts to check for a target around him
        [Header("Maximum attack range")]
        [Range(0f, 35f)] [SerializeField] private float baseAttackRadius; // The distance from which the agent can attack
        [Header("Minimum range needed for attacks")] 
        [Range(0f, 35f)] [SerializeField] private float baseMeleeRadius; // The distance from which the agent goes into combat state
        [Header("Sensing radius for enemy")]
        [Range(0f, 35f)] [SerializeField] private float aggroRange;  // The distance form wich the agent can sense the player after it gets aggro on him
        [Header("Combat radius")]
        [Range(0f, 35f)] [SerializeField] private float combatAggroRange;

        public float SprintSpeed => sprintSpeed;

        public float MaxRotationSpeed => maxRotationSpeed;

        public float PatrolWaitTime => patrolWaitTime;

        public LayerMask DetectionLayer => detectionLayer;

        public LayerMask ObstacleMask => obstacleMask;

        public float DetectionAngle => detectionAngle;

        public float BaseDetectionRadius => baseDetectionRadius;

        public float BaseAttackRadius => baseAttackRadius;

        public float BaseMeleeRadius => baseMeleeRadius;

        public float AggroRange => aggroRange;

        public float CombatAggroRange => combatAggroRange;
    }
}
using EtVK.Actions_Module;
using EtVK.Utyles;
using UnityEngine;
using UnityEngine.Serialization;

namespace EtVK.AI_Module.Actions
{
    [CreateAssetMenu( menuName = "ScriptableObjects/AI/Actions/AttackAction")]
    public class EnemyAttackAction : AttackAction
    {
        [Header("Attack parameters")] 
        [SerializeField] private AttackType attackType;
        [SerializeField] [Range(0f, 1f)] private float transitionDuration = 0.3f;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private int attackScore;
        [SerializeField] private float attackDamage;
        [SerializeField] private float attackCd;

        [Header("Attack requirements")] 
        [SerializeField] [Range(-180f, 180f)] private float minAngle;
        [SerializeField] [Range(-180f, 180f)] private float maxAngle;
        [SerializeField] private float minAttackRange;
        [SerializeField] private float maxAttackRange;

        [Header("The action it can combo into, only used if canCombo true")] 
        [SerializeField] private bool hasCombo;
        [SerializeField] private EnemyAttackAction comboAction;

        public AttackType AttackType => attackType;
        public float TransitionDuration => transitionDuration;
        public float RotationSpeed => rotationSpeed;
        public int AttackScore => attackScore;
        public float AttackDamage => attackDamage;
        public float AttackCd => attackCd;
        public float MinAngle => minAngle;
        public float MaxAngle => maxAngle;
        public float MinAttackRange => minAttackRange;
        public float MaxAttackRange => maxAttackRange;
        public bool HasCombo => hasCombo;
        public EnemyAttackAction ComboAction => comboAction;
    }
}
using UnityEngine;

namespace EtVK.Core
{
    public abstract class BaseAttackController : MonoBehaviour
    {
        public delegate void AttackEvent(Transform position);
        public AttackEvent onAttack;

        protected BaseAttackController currentTarget;
        
        public abstract void OnAttack(Transform position);
        
        public  void Unsubscribe()
        {
            currentTarget.onAttack -= OnAttack;
        }

        public void SetCurrentTarget(BaseAttackController target)
        {
            currentTarget = target;
            currentTarget.onAttack += OnAttack;
        }
    }
}
using EtVK.AI_Module.Managers;
using UnityEngine;

namespace EtVK.AI_Module.Controllers
{
    public class EnemyRootMotionController : MonoBehaviour
    {
        private EnemyManager enemyManager;
        private Animator animator;
        private Vector3 deltaPosition;
        private Quaternion deltaRotation;
        
        
        private void LateUpdate()
        {
            // if (animator.applyRootMotion)
            // {
            //     enemyManager.GetController().Move(deltaPosition);
            //     deltaPosition = Vector3.zero;
            // }
            if (animator.applyRootMotion)
            {
                enemyManager.transform.rotation *= deltaRotation;
                enemyManager.transform.position += deltaPosition;
                deltaPosition = Vector3.zero;
                deltaRotation = Quaternion.identity;
            }
            if (enemyManager.UseRootMotionRotation)
            {
                var weapon = enemyManager.GetInventoryManager().GetCurrentWeapon();
                var speed = weapon?.CurrentAttackAction?.RotationSpeed;
                enemyManager.GetController().RotateTowardsCurrentTarget(speed?? 0f);
            }
        }

        public void Initialize(EnemyManager manager)
        {
            enemyManager = manager;
            animator = enemyManager.GetAnimator();
        }
        
        private void OnAnimatorMove()
        {
            deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0f;
            deltaRotation = animator.deltaRotation;
        }
    }
}
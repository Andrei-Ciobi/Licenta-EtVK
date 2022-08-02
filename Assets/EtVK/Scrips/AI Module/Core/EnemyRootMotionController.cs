﻿using System;
using UnityEngine;

namespace EtVK.AI_Module.Core
{
    public class EnemyRootMotionController : MonoBehaviour
    {
        private EnemyManager enemyManager;
        private Animator animator;
        private Vector3 deltaPosition;
        private Quaternion deltaRotation;
        
        
        private void Update()
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
                enemyManager.GetController().RotateTowardsCurrentTarget();
            }
        }

        private void LateUpdate()
        {

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
﻿using System.Collections.Generic;
using EtVK.AI_Module.Stats;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Player_Module.Controller
{
    public class LockOnController : MonoBehaviour
    {
        [SerializeField] private float maxLockOnDistance;
        [SerializeField] private LayerMask hitLayer;
        [SerializeField] private ActiveCameraEvent changeCameraEvent;

        private PlayerManager playerManager;
        private List<EnemyEntity> availableTargets = new();
        private EnemyEntity currentLockOnTarget;

        private void Awake()
        {
            playerManager = transform.root.GetComponent<PlayerManager>();
        }

        public void LockOnEnemy(ref bool success) 
        {
            if (!CalculateClosestTarget())
                return;

            changeCameraEvent.Invoke(new ActiveCamera(ActiveCameraType.LockOn, currentLockOnTarget.transform));
            currentLockOnTarget.onDie += UnlockFromEnemy;
            success = true;

        }

        public void UnlockFromEnemy()
        {
            changeCameraEvent.Invoke(new ActiveCamera(ActiveCameraType.Main, null));
            currentLockOnTarget.onDie -= UnlockFromEnemy;
            playerManager.GetAnimator().SetBool(PlayerState.IsLockedOn.ToString(), false);
        }

        private bool CalculateClosestTarget()
        {
            var colliders = Physics.OverlapSphere(playerManager.transform.position, 26, hitLayer);
            availableTargets = new List<EnemyEntity>();
            
            // Unsubscribe to the OnDie event and make the current target null
            if (currentLockOnTarget != null)
            {
                currentLockOnTarget.onDie -= UnlockFromEnemy;
                currentLockOnTarget = null;
            }

            foreach (var coll in colliders)
            {
                var entity = coll.GetComponent<EnemyEntity>();
                if(entity == null)
                    continue;
                if(entity.transform.root == playerManager.transform.root)
                    continue;
                
                var targetDirection = entity.transform.position - playerManager.transform.position;
                var distance = Vector3.Distance(playerManager.transform.position , entity.transform.position);
                var angle = Vector3.Angle(targetDirection, playerManager.CameraMainTransform.forward);

                if (angle > -50 && angle < 50 && distance <= maxLockOnDistance)
                {
                    availableTargets.Add(entity);
                }
            }
            
            var shortestDistance = Mathf.Infinity;

            if (availableTargets.Count == 0)
                return false;

            var found = false;
            foreach (var target in availableTargets)
            {
                var distance = Vector3.Distance(playerManager.transform.position, target.transform.position);
                if(distance > shortestDistance)
                    continue;

                shortestDistance = distance;
                currentLockOnTarget = target;
                found = true;
            }

            return found;
        }
        
    }
}
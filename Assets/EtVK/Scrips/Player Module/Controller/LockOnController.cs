using System.Collections.Generic;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Health_Module;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Player_Module.Controller
{
    public class LockOnController : MonoBehaviour
    {
        [SerializeField] private float maxLockOnDistance;
        [SerializeField] private ActiveCameraEvent changeCameraEvent;
        private PlayerManager playerManager;
        private List<LivingEntity> availableTargets = new();
        private LivingEntity currentLockOnTarget;

        private void Awake()
        {
            playerManager = transform.root.GetComponent<PlayerManager>();
        }

        public void LockOnEnemy()
        {
            if (CalculateClosestTarget())
            {
                changeCameraEvent.Invoke(new ActiveCamera(ActiveCameraType.LockOn, currentLockOnTarget.transform));
            }
        }

        private bool CalculateClosestTarget()
        {
            var colliders = Physics.OverlapSphere(playerManager.transform.position, 26);
            availableTargets = new List<LivingEntity>();
            currentLockOnTarget = null;
            
            foreach (var coll in colliders)
            {
                var entity = coll.GetComponent<LivingEntity>();
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
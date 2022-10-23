using System.Collections.Generic;
using EtVK.AI_Module.Actions;
using EtVK.AI_Module.Controllers;
using EtVK.AI_Module.Inventory;
using EtVK.AI_Module.Stats;
using EtVK.AI_Module.Weapons;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.AI_Module.Managers
{
    public class EnemyManager : BaseEnemyManager<EnemyManager, 
                                                EnemyController, 
                                                EnemyInventoryManager,
                                                EnemyLocomotionData,
                                                EnemyEntity>
    {
        private void Awake()
        {
            InitializeBaseReferences();
            rootMotionController.Initialize(this);
            GetLivingEntity().Initialize(this);
        }

        private void Start()
        {
            OnStart();
        }

        public void DecideNextAttack(out bool success)
        {
            CanAttack = false;
            var weapon = inventoryManager.GetCurrentWeapon();

            if (weapon == null)
            {
                CanAttack = true;
                success = false;
                return;
            }
            weapon.CurrentAttackAction = null;
            
            
            var attackActions = weapon.WeaponData.GetActions();
            var canPerformActions = new List<EnemyAttackAction>();
            var maxScore = 0;
            
            attackActions.ForEach(action =>
            {
                if (!InAttackParameters(action)) 
                    return;
                
                maxScore += action.AttackScore;
                canPerformActions.Add(action);
            });

            //If maxScore == 0 => we have no possible attack action
            if (maxScore == 0)
            {
                CanAttack = true;
                success = false;
                return;
            }

            var randomValue = Random.Range(0, maxScore);
            Debug.Log("Max score = " + maxScore + "  ||  randomValue = " + randomValue);
            var temporaryScore = 0;

            foreach (var action in canPerformActions)
            {
                temporaryScore += action.AttackScore;
                if (temporaryScore < randomValue) 
                    continue;
                
                weapon.CurrentAttackAction = action;
                break;
            }

            //Update the animator override controller
            UpdateCurrentAttack(weapon);
            animator.SetBool(EnemyAIAction.IsAttacking.ToString(), true);
            success = true;
        }

        public void UpdateCurrentAttack(EnemyWeapon weapon)
        {
            var overrideController = weapon.WeaponData.AnimatorOverride;
            overrideController[weapon.CurrentAttackAction!.ClipName] = weapon.CurrentAttackAction.AnimationClip;
        }
        
        public bool InAttackParameters(EnemyAttackAction attack)
        {
            // We calculate the current angle and distance from target
            var directionToTarget = controller.DirectionToCurrentTarget;
            var angle = Vector3.Angle(transform.forward, directionToTarget);
            var distanceFromTarget = controller.DistanceToCurrentTarget;

            return distanceFromTarget >= attack.MinAttackRange 
                    && distanceFromTarget <= attack.MaxAttackRange
                    && angle >= attack.MinAngle 
                    && angle <= attack.MaxAngle;
        }
    }
    
}
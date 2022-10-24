using System;
using EtVK.AI_Module.Managers;
using EtVK.Core;
using EtVK.Utyles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EtVK.AI_Module.Controllers
{
    public class EnemyAttackController : BaseAttackController
    {
        public Vector2 Direction => direction;

        private EnemyManager manager;
        private Vector2 direction;

        private void Awake()
        {
            manager = GetComponent<EnemyManager>();
        }

        public override void OnAttack(Transform position)
        {
            if (manager.UninterruptibleAction)
                return;

            if (!(manager.GetController().DistanceToCurrentTarget <=
                  manager.GetLocomotionData().BaseAttackRadius))
                return;
            
            var angleOfTheAttacker = Vector3.SignedAngle(transform.forward, position.forward, Vector3.up);
            direction = CheckAngleDirection(angleOfTheAttacker);
            
            if (direction == Vector2.down)
                return;

            var randomChance = Random.Range(0, 101);
            if(randomChance < 100 - manager.GetLocomotionData().DodgeChance)
                return;
            
            manager.GetAnimationEventController().SetCanCombo(0);
            manager.GetAnimationEventController().DeactivateWeaponCollider();
            manager.GetAnimator().SetFloat(EnemyAIAction.DodgeX.ToString(), -direction.x);
            manager.GetAnimator().SetFloat(EnemyAIAction.DodgeY.ToString(), -direction.y);
            manager.GetAnimator().CrossFade("Dodge State", 0.1f);
        }

        private Vector2 CheckAngleDirection(float angle)
        {
            return angle switch
            {
                >= 145 and <= 180 => Vector2.up,        //Front
                <= -145 and >= -180 => Vector2.up,      //Front
                >= -45 and <= 45 => Vector2.down,       //Back
                >= -145 and <= -45 => Vector2.right,    //Right
                >= 45 and <= 145 => Vector2.left,       //Left
                _ => Vector2.zero                       //Default
            };
        }
    }
}
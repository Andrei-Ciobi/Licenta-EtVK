using System.Collections;
using EtVK.Ability_Module.Core;
using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Ability_Module.Dash
{
    public class DashAbility : BaseAbility
    {
        private void Awake()
        {
            abilityType = AbilityType.Dash;
        }

        public override void PerformAbility(BaseAbilityData baseAbilityData, Animator animator = null,
            Transform obj = null)
        {
            if (onCooldown)
                return;

            onCooldown = true;

            if (animator == null || obj == null)
                return;

            var dashData = (DashAbilityData) baseAbilityData;
            var controller = obj.GetComponent<IMoveComponent>();

            if (controller == null)
            {
                Debug.LogError("No IMoveComponent on object = " + obj.name);
                return;
            }

            if (dashData.AnimationClip == null)
            {
                Debug.LogError("No animation clip for dash ability : " + dashData.name);
                return;
            }

            // Checks if the animation clip has been set if not we exit
            if (!SetAnimatorClip(animator, "Base_ability", dashData.AnimationClip))
                return;

            animator.CrossFade("Base Ability", .1f);

            VFXManager.Instance.PlayPostProcessing(dashData.PostProcessing,
                dashData.Duration, dashData.VFXCurve);
            StartCoroutine(DashForwardCoroutine(dashData, animator, obj, controller));
        }

        private IEnumerator DashForwardCoroutine(DashAbilityData dashData, Animator animator, Transform obj,
            IMoveComponent controller)
        {
            var currentTime = 0f;
            while (dashData.Duration > currentTime)
            {
                var percentage = currentTime / dashData.Duration;
                var speed = dashData.SpeedGraph.Evaluate(percentage) * dashData.Speed;
                controller.Move(obj.forward, speed);
                currentTime += Time.deltaTime;
                yield return null;
            }

            animator.SetBool(AnimatorCommonFileds.AbilityFinished.ToString(), true);

            StartCoroutine(AbilityCooldownCoroutine(dashData.Cooldown));
        }
    }
}
using System.Collections;
using EtVK.Ability_Module.Core;
using EtVK.Core;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Ability_Module.Dash
{
    public class DashAbility : BaseAbility
    {
        private void Awake()
        {
            abilityType = AbilityType.Dash;
        }

        public override void PerformAbility(BaseAbilityData baseAbilityData, Animator animator)
        {
            if (onCooldown)
                return;

            var dashData = (DashAbilityData) baseAbilityData;

            var controller = animator.transform.root.GetComponent<IMoveComponent>();

            if (controller == null)
            {
                Debug.LogError("No IMoveComponent on object = " + animator.transform.root.name);
                return;
            }

            onCooldown = true;
            VFXManager.Instance.PlayPostProcessing(dashData.PostProcessing,
                dashData.Duration, dashData.VFXCurve);
            StartCoroutine(DashForwardCoroutine(dashData, animator, controller));
        }

        private IEnumerator DashForwardCoroutine(DashAbilityData dashData, Animator animator, IMoveComponent controller)
        {
            var currentTime = 0f;
            while (dashData.Duration > currentTime)
            {
                var percentage = currentTime / dashData.Duration;
                var speed = dashData.SpeedGraph.Evaluate(percentage) * dashData.Speed;
                controller.Move(animator.transform.root.forward, speed);
                currentTime += Time.deltaTime;
                yield return null;
            }

            animator.SetBool(AnimatorCommonFileds.AbilityFinished.ToString(), true);

            StartCoroutine(AbilityCooldownCoroutine(dashData.Cooldown));
        }
    }
}
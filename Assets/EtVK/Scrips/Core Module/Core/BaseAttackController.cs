﻿using EtVK.Actions_Module;
using UnityEngine;

namespace EtVK.Core
{
    public abstract class BaseAttackController : MonoBehaviour
    {
        public delegate void AttackEvent(Transform position);

        public AttackEvent onAttack;

        protected BaseAttackController currentTarget;

        public abstract void OnAttack(Transform position);

        public void Unsubscribe()
        {
            currentTarget.onAttack -= OnAttack;
        }

        public void SetCurrentTarget(BaseAttackController target)
        {
            currentTarget = target;
            currentTarget.onAttack += OnAttack;
        }

        public void PlayVisualEffects(AttackAction actionData)
        {
            if(actionData == null)
                return;
            
            VFXManager.Instance.PlayPostProcessing(actionData.PostProcessing, actionData.AnimationClip.length,
                actionData.VFXCurve);
        }
    }
}
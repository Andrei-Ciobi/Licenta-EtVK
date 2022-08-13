using UnityEngine;

namespace EtVK.Actions_Module
{
    public abstract class BaseAction : ScriptableObject
    {
        [SerializeField] private string clipName;
        [SerializeField] private AnimationClip animationClip;
        [SerializeField] private bool canBeInterrupted;

        public string ClipName => clipName;
        public AnimationClip AnimationClip => animationClip;
        public bool CanBeInterrupted => canBeInterrupted;
    }
}
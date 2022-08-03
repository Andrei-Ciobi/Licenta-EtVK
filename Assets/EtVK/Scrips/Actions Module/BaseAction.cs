using UnityEngine;

namespace EtVK.Actions_Module
{
    public abstract class BaseAction : ScriptableObject
    {
        [SerializeField] private string clipName;
        [SerializeField] private AnimationClip animationClip;
        
        public string ClipName => clipName;
        
        public AnimationClip AnimationClip => animationClip;
    }
}
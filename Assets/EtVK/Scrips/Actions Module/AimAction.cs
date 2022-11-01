using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Actions_Module
{
    [CreateAssetMenu( menuName = "ScriptableObjects/Actions/AimAction")]
    public class AimAction : BaseAction
    {
        [Header("Aim action data")] 
        [SerializeField] private WeaponActionType actionType;
        [SerializeField][Range(0f, 1f)] private float transitionDelay;
        [SerializeField] private AnimatorLayer layer;
        [SerializeField] private bool useLayerBlend;
        [SerializeField] private string blendStateName;
        [SerializeField][Range(0f, 1f)] private float blendDelay;
        

        public WeaponActionType ActionType => actionType;
        public int Layer => layer.GetHashCode();
        public bool UseLayerBlend => useLayerBlend;
        public string BlendStateName => blendStateName;
        public float TransitionDelay => transitionDelay;

        public float BlendDelay => blendDelay;
    }
}
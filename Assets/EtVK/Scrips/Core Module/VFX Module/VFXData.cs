using UnityEngine;

namespace EtVK.VFX_Module
{
    public abstract class VFXData : ScriptableObject
    {
        [SerializeField] private AnimationCurve timeCurve;

        public AnimationCurve TimeCurve => timeCurve;
    }
}
using EtVK.Ability_Module.Core;
using UnityEngine;

namespace EtVK.Ability_Module.Dash
{
    [CreateAssetMenu(fileName ="New ability", menuName ="ScriptableObjects/Abilities/Mobility/Dash")]
    public class DashAbilityData : BaseAbilityData
    {
        [Header("Dash ability info")] 
        [SerializeField] private float duration;
        [SerializeField] private float speed;
        [SerializeField] private AnimationCurve speedGraph;

        public float Duration => duration;
        public float Speed => speed;
        public AnimationCurve SpeedGraph => speedGraph;
    }
}
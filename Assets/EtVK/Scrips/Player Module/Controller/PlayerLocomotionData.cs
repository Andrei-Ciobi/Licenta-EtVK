using EtVK.Core_Module;
using UnityEngine;

namespace EtVK.Player_Module.Controller
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/LocomotionData")]
    public class PlayerLocomotionData : LocomotionData
    {

        [SerializeField] private float gravityValue;
        [SerializeField] private float gravityMultiplayer;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float slopeFore = 10f;
        [SerializeField] private float slopeForceRayLenght = 3f;
        [SerializeField] private float steepSlopeLimit = 45f;
        [SerializeField] private float steepSlopeSpeed = 2f;
        [SerializeField] private float pushPower = 5f;

        public float GravityValue => gravityValue;

        public float GravityMultiplayer => gravityMultiplayer;

        public float JumpHeight => jumpHeight;

        public float SlopeFore => slopeFore;

        public float SlopeForceRayLenght => slopeForceRayLenght;

        public float SteepSlopeLimit => steepSlopeLimit;

        public float SteepSlopeSpeed => steepSlopeSpeed;

        public float PushPower => pushPower;
    }
}
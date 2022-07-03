using UnityEngine;

namespace EtVK.Scrips.Player_Module.Controller
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/LocomotionData")]
    public class PlayerLocomotionData : ScriptableObject
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float gravityValue;
        [SerializeField] private float gravityMultiplayer;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float slopeFore = 10f;
        [SerializeField] private float slopeForceRayLenght = 3f;


        public float GetWalkSpeed()
        {
            return walkSpeed;
        }

        public float GetRunSpeed()
        {
            return runSpeed;
        }
        public float GetRotationSpeed()
        {
            return rotationSpeed;
        }

        public float GetGravityValue()
        {
            return gravityValue;
        }

        public float GetGravityMultiplayer()
        {
            return gravityMultiplayer;
        }

        public float GetJumpHeight()
        {
            return jumpHeight;
        }

        public float GetSlopeForce()
        {
            return slopeFore;
        }

        public float GetSlopeForceRayLenght()
        {
            return slopeForceRayLenght;
        }
    }
}
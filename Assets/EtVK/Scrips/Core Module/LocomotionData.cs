using UnityEngine;

namespace EtVK.Core_Module
{
    public abstract class LocomotionData : ScriptableObject
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float rotationSpeed;

        public float WalkSpeed => walkSpeed;

        public float RunSpeed => runSpeed;

        public float RotationSpeed => rotationSpeed;
    }
}
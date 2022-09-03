using UnityEngine;

namespace EtVK.Core
{
    public abstract class LocomotionData : ScriptableObject
    {
        [SerializeField] private float walkSlowSpeed;
        [SerializeField] private float walkFastSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float rotationSpeed;

        public float WalkSlowSpeed => walkSlowSpeed;

        public float WalkFastSpeed => walkFastSpeed;

        public float RunSpeed => runSpeed;

        public float RotationSpeed => rotationSpeed;
    }
}
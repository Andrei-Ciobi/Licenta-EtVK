using UnityEngine;

namespace EtVK.Core
{
    public abstract class LocomotionData : ScriptableObject
    {
        [SerializeField] private float walkSlowSpeed;
        [SerializeField] private float walkFastSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float bonusSpeed;

        public float WalkSlowSpeed => walkSlowSpeed + bonusSpeed;
        public float WalkFastSpeed => walkFastSpeed + bonusSpeed;
        public float RunSpeed => runSpeed + bonusSpeed;
        public float RotationSpeed => rotationSpeed;

        public void AddBonusSpeed(float value)
        {
            bonusSpeed += value;
        }
    }
}
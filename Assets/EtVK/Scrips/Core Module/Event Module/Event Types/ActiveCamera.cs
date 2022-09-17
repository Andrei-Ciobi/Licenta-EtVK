using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Event_Module.Event_Types
{
    [System.Serializable] public struct ActiveCamera
    {
        public ActiveCameraType CameraType { get; set; }
        public Transform TargetTransform { get; set; }

        public ActiveCamera(ActiveCameraType cameraType, Transform targetTransform)
        {
            CameraType = cameraType;
            TargetTransform = targetTransform;
        }
    }
}
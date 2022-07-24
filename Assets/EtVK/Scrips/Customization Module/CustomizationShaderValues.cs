using UnityEngine;

namespace EtVK.Customization_Module
{
    [System.Serializable]
    public class CustomizationShaderValues
    {
        [Range(0f, 1f)] public float metalic;
        [Range(0f, 1f)] public float smoothness;
        [Range(0f, 1f)] public float emmision;
    }
}
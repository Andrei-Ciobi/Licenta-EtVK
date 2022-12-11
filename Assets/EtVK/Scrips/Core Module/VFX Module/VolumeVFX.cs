using UnityEngine;
using UnityEngine.Rendering;

namespace EtVK.VFX_Module
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Vfx/Volume")]
    public class VolumeVFX : VFXData
    {
        [SerializeField] private GameObject volume;
        public GameObject Obj => volume;
    }
}
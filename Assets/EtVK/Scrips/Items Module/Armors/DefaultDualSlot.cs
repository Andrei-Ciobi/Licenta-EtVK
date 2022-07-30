using UnityEngine;

namespace EtVK.Items_Module.Armors
{
    public class DefaultDualSlot : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer leftMesh;
        [SerializeField] private SkinnedMeshRenderer rightMesh;

        public SkinnedMeshRenderer LeftMesh => leftMesh;

        public SkinnedMeshRenderer RightMesh => rightMesh;
    }
}
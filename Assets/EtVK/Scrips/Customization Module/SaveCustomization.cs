using UnityEngine;

namespace EtVK.Customization_Module
{
    public abstract class SaveCustomization : MonoBehaviour
    {
        public abstract void Save();

        protected GameObject PreparePrefab(SkinnedMeshRenderer obj, Material mat)
        {
            var parentObj = new GameObject
            {
                transform =
                {
                    parent = obj.transform.parent,
                    position = Vector3.zero,
                    rotation = Quaternion.identity,
                    localScale = Vector3.one
                },
                name = "parent"
            };

            var newObj = new GameObject
            {
                transform =
                {
                    parent = parentObj.transform,
                    position = Vector3.zero,
                    rotation = Quaternion.identity,
                    localScale = Vector3.one
                },
                name = gameObject.name + "_meshFilter"
            };
            var meshFilter = newObj.AddComponent<MeshFilter>().GetComponent<MeshFilter>();
            var meshRenderer = newObj.AddComponent<MeshRenderer>().GetComponent<MeshRenderer>();
            meshFilter.mesh = obj.sharedMesh;
            meshRenderer.material = mat;

            var cloneObj = Instantiate(obj.gameObject, parentObj.transform, false);
            cloneObj.name = obj.name;
            cloneObj.transform.position = Vector3.zero;
            cloneObj.transform.rotation = Quaternion.identity;
            // cloneObj.transform.localScale = Vector3.one;
            cloneObj.SetActive(false);

            
            return parentObj;
        }
    }
}
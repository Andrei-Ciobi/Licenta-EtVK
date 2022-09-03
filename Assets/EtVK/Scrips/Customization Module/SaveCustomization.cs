using System.IO;
using UnityEditor;
using UnityEngine;

namespace EtVK.Customization_Module
{
    public abstract class SaveCustomization : MonoBehaviour
    {
        [SerializeField] protected string prefabName;
        [SerializeField] protected string fullPath;
        [SerializeField] protected string folderName;
        public abstract void Save();

        protected GameObject PreparePrefab(SkinnedMeshRenderer obj, Material mat, string parentName)
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
                name = parentName
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

            var cloneObj = Instantiate(obj, parentObj.transform, false);
            cloneObj.name = obj.name;
            cloneObj.transform.position = Vector3.zero;
            cloneObj.transform.rotation = Quaternion.identity;
            cloneObj.material = mat;
            // cloneObj.transform.localScale = Vector3.one;
            cloneObj.gameObject.SetActive(false);


            return parentObj;
        }

        protected string CheckAndCreateDirectory(string fullPath, string folderName, string prefabName)
        {
            if (!Directory.Exists($"{fullPath}/{folderName}"))
                AssetDatabase.CreateFolder($"{fullPath}", folderName);

            var localPath = $"{fullPath}/{folderName}/" + "Model_" + prefabName + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            return localPath;
        }

#if UNITY_EDITOR
        protected void SavePrefab(GameObject prefab, string localPath)
        {
            PrefabUtility.SaveAsPrefabAsset(prefab, localPath, out var prefabSuccess);


            Debug.Log(prefabSuccess ? "Prefab was saved successfully" : "Prefab failed to save");
        }
#endif
    }
}
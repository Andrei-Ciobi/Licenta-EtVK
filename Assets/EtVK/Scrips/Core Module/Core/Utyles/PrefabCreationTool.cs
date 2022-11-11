using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EtVK.Core.Utyles
{
#if UNITY_EDITOR
    public class PrefabCreationTool : MonoBehaviour
    {
        [Header("Prefab props")] 
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Material material;
        [SerializeField] private bool resetTransform;
        [SerializeField] private bool isStatic;
        [Header("Default directory " + "Assets/EtVK/Prefab/Env")] 
        [SerializeField] private string prefabName;
        [SerializeField] private string folderName;
        [SerializeField] private List<GameObject> models = new();


        private static readonly string path = "Assets/EtVK/Prefab/Env";

        public void Save()
        {
            foreach (var model in models)
            {
                var obj = CreatePrefab(model, prefabName);
                if (obj.transform.childCount > 0)
                {
                    SetChildrenColliders(obj);
                }
                else
                {
                    obj.AddComponent<MeshCollider>();
                }
                
                SetPrefabProps(obj);
                
                var localPath = CheckAndCreateDirectory(path, folderName, prefabName);
                SavePrefab(obj, localPath);
                DestroyImmediate(obj);
                
            }
        }

        private void SetChildrenColliders(GameObject obj)
        {
            for (var index = 0; index < obj.transform.childCount; index++)
            {
                obj.transform.GetChild(index).gameObject.AddComponent<MeshCollider>();
                obj.transform.GetChild(index).gameObject.isStatic = isStatic;
            }
        }

        private GameObject CreatePrefab(GameObject model, string newName)
        {
            var obj = Instantiate(model);
            
            // PrefabUtility.UnpackPrefabInstance(obj, PrefabUnpackMode.Completely, InteractionMode.UserAction);
            if (resetTransform)
            {
                obj.transform.position = Vector3.zero;
                obj.transform.rotation = Quaternion.identity;
            }
            // obj.transform.localScale = Vector3.one;
            
            obj.name = newName;
            obj.isStatic = isStatic;

            return obj;
        }

        private void SetPrefabProps(GameObject obj)
        {
            obj.layer = layerMask;
            if (obj.transform.childCount > 0)
            {
                for (var index = 0; index < obj.transform.childCount; index++)
                {
                    obj.transform.GetChild(index).gameObject.layer = layerMask;
                    obj.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material = material;
                }
            }
            else
            {
                obj.GetComponent<MeshRenderer>().material = material;
            }
        }
        
        private string CheckAndCreateDirectory(string fullPath, string folderName, string prefabName)
        {
            if (!Directory.Exists($"{fullPath}/{folderName}"))
                AssetDatabase.CreateFolder($"{fullPath}", folderName);
            
            var localPath = $"{fullPath}/{folderName}/" + "Prefab_" + prefabName + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            return localPath;
        }
        
        private void SavePrefab(GameObject prefab, string localPath)
        {
            PrefabUtility.SaveAsPrefabAsset(prefab, localPath, out var prefabSuccess);
            
            
            Debug.Log(prefabSuccess ? "Prefab was saved successfully" : "Prefab failed to save");
        }
        
    }
    #endif
}
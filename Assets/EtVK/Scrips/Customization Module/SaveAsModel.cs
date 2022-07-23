using System.IO;
using UnityEditor;
using UnityEngine;

namespace EtVK.Customization_Module
{
    public class SaveAsModel : SaveCustomization
    {
        [SerializeField] private string prefabName;
        [SerializeField] private string fullPath;
        [SerializeField] private string folderName;
        
        public override void Save()
        {
            var modEleOpt = GetComponent<ModularElementOption>();

            if (modEleOpt.GetCurrentElement() == null)
            {
                Debug.Log("No active element");
                return;
            }
            
            
            if (!Directory.Exists($"{fullPath}/{folderName}"))
                AssetDatabase.CreateFolder($"{fullPath}", folderName);
            
            var localPath = $"{fullPath}/{folderName}/" + "Model_" + prefabName + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            var prefab = PreparePrefab(modEleOpt.GetCurrentElement(), modEleOpt.Mat);

            PrefabUtility.SaveAsPrefabAsset(prefab, localPath, out var prefabSuccess);
            
            
            Debug.Log(prefabSuccess ? "Prefab was saved successfully" : "Prefab failed to save");
            
            DestroyImmediate(prefab);
        }
        
    }
}
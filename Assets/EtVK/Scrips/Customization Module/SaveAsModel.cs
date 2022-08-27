using UnityEngine;

namespace EtVK.Customization_Module
{
    public class SaveAsModel : SaveCustomization
    {
#if UNITY_EDITOR
        public override void Save()
        {
            var modEleOpt = GetComponent<ModularElementOption>();

            if (modEleOpt.GetCurrentElement() == null)
            {
                Debug.Log("No active element");
                return;
            }

            var localPath = CheckAndCreateDirectory(fullPath, folderName, prefabName);

            var prefab = PreparePrefab(modEleOpt.GetCurrentElement(), modEleOpt.Mat,
                modEleOpt.GetCurrentElement().gameObject.name);
            SavePrefab(prefab, localPath);


            DestroyImmediate(prefab);
        }
#endif
    }
}
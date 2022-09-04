using UnityEngine;

namespace EtVK.Customization_Module
{
#if UNITY_EDITOR
    public class SaveAsModel : SaveCustomization
    {

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

    }
#endif
}
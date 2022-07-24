using System.IO;
using UnityEditor;
using UnityEngine;

namespace EtVK.Customization_Module
{
    public class SaveDualAsModel : SaveCustomization
    {
        [SerializeField] private string prefabName;
        [SerializeField] private string fullPath;
        [SerializeField] private string folderName;
        public override void Save()
        {
            var modEleOptList = GetComponentsInChildren<ModularElementOption>();

            if (modEleOptList.Length != 2)
            {
                Debug.Log("Not enough active elements");
                return;
            }

            var localPath = CheckAndCreateDirectory(fullPath, folderName, prefabName);

            var firstObj = PreparePrefab(modEleOptList[0].GetCurrentElement(), modEleOptList[0].Mat);
            var secondObj = PreparePrefab(modEleOptList[1].GetCurrentElement(), modEleOptList[1].Mat);

            var prefab = CreateParentPrefab(firstObj.transform, secondObj.transform);
            
            SavePrefab(prefab, localPath);
            
            DestroyImmediate(prefab);

        }

        private GameObject CreateParentPrefab(Transform firstObj, Transform secondObj)
        {
            var parentObj = new GameObject()
            {
                transform =
                {
                    parent = transform,
                    position = Vector3.zero,
                    rotation = Quaternion.identity,
                    localScale = Vector3.one
                }
            };

            firstObj.parent = parentObj.transform;
            firstObj.position = Vector3.zero;
            firstObj.rotation = Quaternion.identity;
            
            secondObj.parent = parentObj.transform;
            secondObj.position = Vector3.zero;
            secondObj.rotation = Quaternion.identity;

            return parentObj;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EtVK.Customization_Module
{
    public class SaveMultipleAsModel : SaveCustomization
    {
        public override void Save()
        {
            var modEleOptList = GetComponentsInChildren<ModularElementOption>().ToList();

            if (modEleOptList.Count == 0)
            {
                Debug.Log("Not enough active elements");
                return;
            }

            var localPath = CheckAndCreateDirectory(fullPath, folderName, prefabName);

            var preparedPrefabList = new List<Transform>();

            foreach (var option in modEleOptList)
            {
                var obj = PreparePrefab(option.GetCurrentElement(), option.Mat,
                    option.GetCurrentElement().gameObject.name);
                preparedPrefabList.Add(obj.transform);
            }

            var prefab = CreateParentPrefab(preparedPrefabList);
            
            SavePrefab(prefab, localPath);
            
            DestroyImmediate(prefab);

        }

        private GameObject CreateParentPrefab(List<Transform> objList)
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

            objList.ForEach(x =>
            {
                x.parent = parentObj.transform;
                x.position = Vector3.zero;
                x.rotation = Quaternion.identity;
            });
            

            return parentObj;
        }
    }
}
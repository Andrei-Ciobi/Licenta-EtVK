﻿using System.IO;
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

            var localPath = CheckAndCreateDirectory(fullPath, folderName, prefabName);

            var prefab = PreparePrefab(modEleOpt.GetCurrentElement(), modEleOpt.Mat);
            SavePrefab(prefab, localPath);
            
            
            DestroyImmediate(prefab);
        }
        
    }
}
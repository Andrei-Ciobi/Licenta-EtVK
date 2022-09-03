using System.Linq;
using UnityEngine;

namespace EtVK.Customization_Module
{
    public class SaveFullModel : SaveCustomization
    {
        [SerializeField] private GameObject configurationModel;
#if UNITY_EDITOR
        public override void Save()
        {
            if (configurationModel == null)
                return;

            var localPath = CheckAndCreateDirectory(fullPath, folderName, prefabName);

            var prefab = Instantiate(configurationModel);
            var modChrOpt = prefab.GetComponentInChildren<ModularCharacterOptions>();
            var modEleOptList = prefab.GetComponentsInChildren<ModularElementOption>().ToList();

            modChrOpt.Initialize();
            modEleOptList.ForEach(x => x.DestroyInactive());

            prefab.transform.position = Vector3.zero;
            prefab.transform.rotation = Quaternion.identity;
            prefab.transform.localScale = Vector3.one;
            SavePrefab(prefab, localPath);
            DestroyImmediate(prefab);
        }
#endif
    }
}
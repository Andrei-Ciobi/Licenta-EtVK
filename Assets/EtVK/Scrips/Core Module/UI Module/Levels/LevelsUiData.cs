using System;
using System.Collections.Generic;
using EtVK.Core.Utyles;
using EtVK.UI_Module.Core;
using UnityEngine;

namespace EtVK.UI_Module.Levels
{
    [CreateAssetMenu( menuName = "ScriptableObjects/UiData/Levels")]
    public class LevelsUiData : BaseUiData
    {
        [SerializeField] private Texture2D defaultLevelImg;
        [SerializeField] private List<SerializableSet<GameLevel, string>> levelNames;
        [SerializeField] private List<SerializableSet<GameLevel, Texture2D>> levelImgList;
        
        public int TotalLevels => levelNames.Count;

        public string GetLevelName(GameLevel level)
        {
            return levelNames.Find(x => x.GetKey() == level)?.GetValue() ?? "Not set";
        }

        public Texture2D GetLevelImg(GameLevel level)
        {
            return levelImgList.Find(x => x.GetKey() == level)?.GetValue() ?? defaultLevelImg;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace EtVK.Core.Utyles
{
    [CreateAssetMenu( menuName = "ScriptableObjects/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private string version;
        [SerializeField] private Texture2D defaultLevelImg;
        [SerializeField] private List<SerializableSet<GameLevel, string>> levelNames;
        [SerializeField] private List<SerializableSet<GameLevel, Texture2D>> levelImgList;
        public string Version => version;
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
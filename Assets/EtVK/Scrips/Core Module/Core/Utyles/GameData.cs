using UnityEngine;

namespace EtVK.Core.Utyles
{
    [CreateAssetMenu( menuName = "ScriptableObjects/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private bool fullGame;
        [SerializeField] private string version;
       
        public string Version => version;
        public bool FullGame => fullGame;
    }
}
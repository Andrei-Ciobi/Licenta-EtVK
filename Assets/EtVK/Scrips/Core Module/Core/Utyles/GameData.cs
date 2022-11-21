using System.Collections.Generic;
using UnityEngine;

namespace EtVK.Core.Utyles
{
    [CreateAssetMenu( menuName = "ScriptableObjects/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private string version;
       
        public string Version => version;
       
    }
}
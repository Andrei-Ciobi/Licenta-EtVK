using EtVK.Player_Module.Manager;
using UnityEngine;

namespace EtVK.Upgrades_Module.Core
{
    public abstract class BaseUpgradeData : ScriptableObject
    {
        [Header("Base data")] 
        [SerializeField] private Texture2D icon;
        [SerializeField] private string title;
        [SerializeField] [TextArea(5, 15)] protected string description;

        public Texture2D Icon => icon;
        public string Title => title;
        public string Description => description;

        public abstract string GetDescriptionFormatted();
        public abstract void Action(PlayerManager manager);
    }
}
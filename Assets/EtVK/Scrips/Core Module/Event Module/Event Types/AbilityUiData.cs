using EtVK.Core.Utyles;
using EtVK.Event_Module.Events;
using UnityEngine;

namespace EtVK.Event_Module.Event_Types
{
    [System.Serializable] public struct AbilityUiData
    {
        public AbilityButtonType ButtonType { get; set; }
        public Texture2D Icon { get; set; }
        public AbilityUiEvent UpdateEvent { get; set; }
        public int CdTime { get; set; }
        public float CdPercentage { get; set; }

        public AbilityUiData(AbilityButtonType buttonType, Texture2D icon, AbilityUiEvent updateEvent) : this()
        {
            ButtonType = buttonType;
            Icon = icon;
            UpdateEvent = updateEvent;
        }

        public AbilityUiData(int cdTime, float cdPercentage) : this()
        {
            CdTime = cdTime;
            CdPercentage = cdPercentage;
        }
    }
}
using EtVK.Upgrades_Module.Core;
using UnityEngine.Events;

namespace EtVK.Event_Module.Custom_Events
{
    [System.Serializable] public class CustomUpgradeEvent : UnityEvent<BaseUpgradeData> { }
}
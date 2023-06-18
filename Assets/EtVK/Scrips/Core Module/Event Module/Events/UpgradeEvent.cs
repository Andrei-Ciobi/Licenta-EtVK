using EtVK.Event_Module.Core;
using EtVK.Upgrades_Module.Core;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New GameUi Event", menuName = "ScriptableObjects/Events/UpgradeEvent")]
    public class UpgradeEvent : BaseGameEvent<BaseUpgradeData> { }
}
using EtVK.Event_Module.Core;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New GameUi Event", menuName = "ScriptableObjects/Events/StringEvent")]
    public class StringEvent : BaseGameEvent<string> { }
}
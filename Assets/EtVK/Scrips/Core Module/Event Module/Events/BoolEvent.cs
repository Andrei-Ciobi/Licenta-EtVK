using EtVK.Event_Module.Core;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "ScriptableObjects/Events/BoolEvent")]
    public class BoolEvent : BaseGameEvent<bool> { }
}
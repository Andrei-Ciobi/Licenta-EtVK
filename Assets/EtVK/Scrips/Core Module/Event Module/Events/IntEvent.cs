using EtVK.Event_Module.Core;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "ScriptableObjects/Events/IntEvent")]
    public class IntEvent : BaseGameEvent<int> { }
}

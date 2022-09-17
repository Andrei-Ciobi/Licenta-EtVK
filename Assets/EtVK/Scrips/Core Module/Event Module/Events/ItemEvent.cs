using EtVK.Event_Module.Event_Types;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName ="ScriptableObjects/Events/ItemEvent")]
    public class ItemEvent : BaseGameEvent<Item>{}
}
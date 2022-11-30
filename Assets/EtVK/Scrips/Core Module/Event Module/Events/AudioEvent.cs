using EtVK.Event_Module.Core;
using EtVK.Event_Module.Event_Types;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "ScriptableObjects/Events/AudioEvent")]
    public class AudioEvent : BaseGameEvent<AudioData> { }
}
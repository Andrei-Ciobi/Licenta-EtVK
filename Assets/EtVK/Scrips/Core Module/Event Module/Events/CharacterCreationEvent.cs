using EtVK.Event_Module.Core;
using EtVK.Event_Module.Event_Types;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName ="ScriptableObjects/Events/CharacterCreation")]
    public class CharacterCreationEvent : BaseGameEvent<CharacterCreationData> { }
}

using EtVK.Event_Module.Core;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "ScriptableObjects/Events/VectorThreeEvent")]
    public class VectorThreeEvent : BaseGameEvent<Vector3> { }
}
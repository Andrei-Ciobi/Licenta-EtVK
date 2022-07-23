using EtVK.Event_Module.Event_Types;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName ="ScriptableObjects/Events/VoidEvent")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Invoke() => Invoke(new Void());
    }
}
